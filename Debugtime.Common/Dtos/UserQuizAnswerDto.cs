using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Dtos
{
    public class UserQuizAnswerDto
    {
        public string UserId { get; set; }
        public string QuizId { get; set; }
        public string QuestionKey { get; set; }
        public string Answer { get; set; }
        public string AnswerKey { get; set; }
        public bool IsCorrect { get; set; }
    }
}
