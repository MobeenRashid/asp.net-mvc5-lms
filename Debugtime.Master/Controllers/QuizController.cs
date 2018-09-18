using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Debugtime.Common.Persistence;
using Debugtime.Master.Models.Input;
using DebugTime.Domain.Model;

namespace Debugtime.Master.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private ApplicationDbContext _context;

        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(bool isOk = false)
        {
            ViewBag.Success = isOk;
            _context = new ApplicationDbContext();

            var courses = _context.Courses.Select(c => new { c.Id, c.Title }).ToList();
            ViewBag.Courses = courses.Select(c => new SelectListItem { Value = c.Id, Text = c.Title, });

            var quizes = _context.Quizes.ToList();
            ViewBag.Quizes = quizes.Select(q => new SelectListItem { Value = q.QuizId, Text = q.Title });

            ViewBag.Questions = new List<SelectListItem>();


            return View();
        }

        [HttpPost]
        public ActionResult AddQuiz(QuizInputModel model)
        {
            ViewBag.Success = false;
            _context = new ApplicationDbContext();

            var qModel = new QuizQuestionOptionModel
            {
                QuizTitle = model.QuizTitle,
                CourseId = model.CourseId
            };

            try
            {
                var courses = _context.Courses.Select(c => new { c.Id, c.Title }).ToList();
                ViewBag.Courses = courses.Select(c => new SelectListItem { Value = c.Id, Text = c.Title });

                var quizes = _context.Quizes.ToList();
                ViewBag.Quizes = quizes.Select(q => new SelectListItem { Value = q.QuizId, Text = q.Title });

                ViewBag.Questions = new List<SelectListItem>();

                if (!ModelState.IsValid)
                    return View("Add", qModel);

                var course = _context.Courses.SingleOrDefault(c => c.Id == model.CourseId);

                if (course != null && !course.HasAssessment)
                {
                    var quiz = new Quiz
                    {
                        QuizId = model.CourseId,
                        Title = model.QuizTitle
                    };

                    course.Quiz = quiz;
                    course.HasAssessment = true;
                }
                else
                {
                    ModelState.AddModelError("QuizTitle", @"Course can only have one quiz.");
                    return View("Add", qModel);
                }

                _context.Entry(course).State = EntityState.Modified;

                if (_context.SaveChanges() > 0)
                    ViewBag.Success = true;
                else
                {
                    ModelState.AddModelError("", @"Failed to add quiz, please try again");
                    return View("Add", qModel);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("QuizTitle", @"Course can only have one quiz.");
            }

            return RedirectToAction("Add", new { isOK = true });
        }

        [HttpPost]
        public ActionResult AddQuestion(QuizQuestionInputModel model)
        {
            ViewBag.Success = false;

            _context = new ApplicationDbContext();

            var qModel = new QuizQuestionOptionModel
            {
                QuestionTitle = model.QuestionTitle,
                QuestionAnswer = model.QuestionAnswer,
                QuizId = model.QuizId
            };
            

            try
            {
                var courses = _context.Courses.Select(c => new { c.Id, c.Title }).ToList();
                ViewBag.Courses = courses.Select(c => new SelectListItem { Value = c.Id, Text = c.Title });

                var quizes = _context.Quizes.ToList();
                ViewBag.Quizes = quizes.Select(q => new SelectListItem { Value = q.QuizId, Text = q.Title });

                ViewBag.Questions = new List<SelectListItem>();


                if (!ModelState.IsValid)
                    return View("Add", qModel);

                var qCount = _context.QuizQuestions.Count(q => q.QuizId == model.QuizId);
                var quizQuestion = new QuizQuestion
                {
                    Title = model.QuestionTitle,
                    QuizId = model.QuizId,
                    Answer = model.QuestionAnswer,
                    Key = model.QuestionTitle,
                    Order = ++qCount
                };

                _context.QuizQuestions.Add(quizQuestion);

                if (_context.SaveChanges() <= 0)
                {
                    ModelState.AddModelError("QuestionTitle", @"Failed to add Question");
                    return View("Add", qModel);
                }

                ViewBag.Success = true;
                return View("Add");
            }
            catch (Exception)
            {
                ModelState.AddModelError("QuestionTitle", @"Cannot add duplicate question in same quiz.");
                return View("Add", qModel);
            }
        }

        [AllowAnonymous]
        public ActionResult GetQuestions(string id)
        {
            _context = new ApplicationDbContext();

            return Json(_context.QuizQuestions.Where(q => q.QuizId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOption(QuestionOptionInputModel model)
        {
            ViewBag.Success = false;
            _context = new ApplicationDbContext();

            var courses = _context.Courses.Select(c => new { c.Id, c.Title }).ToList();
            ViewBag.Courses = courses.Select(c => new SelectListItem { Value = c.Id, Text = c.Title, });

            var quizes = _context.Quizes.ToList();
            ViewBag.Quizes = quizes.Select(q => new SelectListItem { Value = q.QuizId, Text = q.Title });

            ViewBag.Questions = new List<SelectListItem>();

            var qModel = new QuizQuestionOptionModel
            {
                QuizId = model.QuestionQuizId,
                QuestionTitle = model.OptionQuestionTitle,
                OptionValue = model.OptionValue,
                QuestionId = model.QuestionId
            };

            try
            {
                if (!ModelState.IsValid)
                    return View("Add", qModel);
               
                var newOption = new QuestionOption
                {
                    Value = model.OptionValue,
                    QuestionId = model.QuestionId,
                    QuestionTitle = model.OptionQuestionTitle,
                    Key = model.OptionValue,
                    QuestionKey = model.OptionQuestionTitle
                };

                _context.QuestionOptions.Add(newOption);

                if (_context.SaveChanges() <= 0)
                    ModelState.AddModelError("", @"Failed to update question options, please try again.");
                else
                {
                    ViewBag.Success = true;
                    return View("Add");
                }

                return View("Add", qModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError("OptionValue", @"Cannot add duplicate option value in same quiz question.");
                return View("Add", qModel);
            }
        }
    }
}