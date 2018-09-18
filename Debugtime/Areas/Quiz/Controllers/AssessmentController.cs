using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Debugtime.Common.Persistence;
using AutoMapper;
using Debugtime.Common.Configurations;
using Debugtime.Common.Infrastructure;
using Debugtime.Common.Model.View;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Debugtime.Areas.Quiz.Controllers
{
    [RouteArea("Quiz")]
    public class AssessmentController : Controller
    {
        private ApplicationDbContext _context;
        private AppUserManager _userManager;

        private IMapper _mapper;
        public IMapper Mapper =>
            _mapper ?? (_mapper = new AutoMapperProfileConfiguration().EntityMapper);
        public AppUserManager UserManager =>
            _userManager ?? (_userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>());

        [Route("{quizId}/question/{question}")]
        public async Task<ActionResult> Index(string quizId, int question = 1)
        {
            if (string.IsNullOrEmpty(quizId))
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        Subject = "Quiz Not Found",
                        Message = "Assessment you are trying to take not exist.",
                        HelpUrl = Url.Action("Index", "Courses", new { area = "" }),
                        area = ""
                    });

            _context = new ApplicationDbContext();

            var quiz = _context.Quizes.SingleOrDefault(q => q.QuizId == quizId);
            
            if (quiz == null)
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        Subject = "Quiz Not Found",
                        Message = "Assessment you are trying to take not exist.",
                        HelpUrl = Url.Action("Index", "Courses", new { area = "" }),
                        UrlText = "Try Again",
                        area = ""
                    });

            if (!await UserManager.HasCourseAsync(quiz.QuizId, User.Identity.GetUserId()))
                return RedirectToAction("Detail", "Courses", new { courseId = quiz.QuizId,area="Library" });

            if (_context.Users.Any(u => u.Assesments.Any(a => a.QuizId == quiz.QuizId)))
                return RedirectToAction("Result", new { quizId = quiz.QuizId });

            var quizQuestion = _context.QuizQuestions.SingleOrDefault(qu => qu.Order == question);

            var questionOptions = _context.QuestionOptions.Where(qo => qo.QuestionId == quizQuestion.QuizId && qo.QuestionTitle == quizQuestion.Title).ToList();

            var viewModel = Mapper.Map<DebugTime.Domain.Model.Quiz, AssessmentQuizViewModel>(quiz);
            viewModel.Question = Mapper.Map<QuizQuestion, AssessmentQuestionViewModel>(quizQuestion);
            viewModel.Question.QuestionOptions = questionOptions.Select(qo =>
                 Mapper.Map<QuestionOption, AssessmentQuestionOptionViewModel>(qo));

            viewModel.SetNextQuestion(question);

            viewModel.RemainingQuestions = _context.QuizQuestions.Count(qu => qu.QuizId == quizId) - question;

            return View("Quiz", viewModel);
        }


        [Route("{quizId}/result")]
        public ActionResult Result(string quizId)
        {
            _context = new ApplicationDbContext();

            var quiz = _context.Quizes.SingleOrDefault(q => q.QuizId == quizId);

            if (quiz == null)
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        Subject = "Quiz Result Not Found",
                        Message = "Assessment result you are trying to access not exist.",
                        HelpUrl = Url.Action("Index", "Courses", new { area = "" }),
                        UrlText = "Go back"
                    });

            var userId = User.Identity.GetUserId();

          

            var questionsCount = _context.QuizQuestions.Count(q => q.QuizId == quizId);
            var userAnswers = _context.UserQuizAnswers.Where(a => a.QuizId == quizId && a.UserId == userId).ToList();

            if (userAnswers.Count <= 0)
                return RedirectToAction("Error", "Oops",
                    new
                    {
                        Subject = "Quiz Result Not Found",
                        Message = "Assessment result you are trying to access not be taken.",
                        HelpUrl = Url.Action("Index", "Assessment", new { area = "Quiz", quizId = quiz.QuizId, question = 1 }),
                        UrlText = "Take Assessment",
                        area = ""
                    });

            if (!_context.Users.Any(u => u.Assesments.Any(a => a.QuizId == quiz.QuizId)))
            {
                var currentUser = _context.Users.SingleOrDefault(u => u.Id == userId);
                currentUser?.Assesments.Add(quiz);
                _context.Entry(currentUser).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var correctAnswers = userAnswers.Count(a => a.IsCorrect);

            var totalScore = questionsCount * 5;
            var passingScore = (int)(totalScore / 1.5);
            var yourScore = correctAnswers * 5;

            var succeeded = passingScore <= yourScore;
            var status = passingScore <= yourScore ? "passed" : "failed";
            var viewModel = new UserQuizResultViewModel
            {
                QuizId = quiz.QuizId,
                Title = quiz.Title,
                Succeeded = succeeded,
                CandidateId = userId,
                PassingScore = passingScore,
                YourScore = yourScore,
                Status = status
            };

            return View(viewModel);
        }
    }
}