using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity.Owin;

namespace Debugtime.Common.Model.View
{
    public class AssessmentQuizViewModel
    {
        private readonly ApplicationDbContext _context;
        public AssessmentQuizViewModel()
        {
            Question = new AssessmentQuestionViewModel();
            _context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }
        public string QuizId { get; set; }
        public string Title { get; set; }
        public bool HasNext { get; set; }
        public void SetNextQuestion(int question)
        {
            ++question;

            if (_context.QuizQuestions.Any(c => c.QuizId==QuizId&&c.Order==question))
            {
                HasNext = true;
                NextQuestion = question;
            }
            else
            {
                HasNext = false;
            }
        }

        public int RemainingQuestions { get; set; }
        public int NextQuestion { get; set; }

        public AssessmentQuestionViewModel Question { get; set; }
    }
}

