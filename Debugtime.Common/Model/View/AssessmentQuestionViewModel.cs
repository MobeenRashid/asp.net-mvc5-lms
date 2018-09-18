using System.Collections.Generic;

namespace Debugtime.Common.Model.View
{
    public class AssessmentQuestionViewModel
    {
        public AssessmentQuestionViewModel()
        {
            QuestionOptions = new HashSet<AssessmentQuestionOptionViewModel>();
        }

        public string QuizId { get; set; }

        public string Key { get; set; }

        public string Title { get; set; }

        public string Answer { get; set; }

        public int Order { get; set; }
        
        public IEnumerable<AssessmentQuestionOptionViewModel> QuestionOptions { get; set; }
    }
}