using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Model.View
{
    public class UserQuizResultViewModel
    {
        public bool Succeeded { get; set; }
        public string Title { get; set; }
        public string CandidateId { get; set; }
        public string QuizId { get; set; }
        public int PassingScore { get; set; }
        public int YourScore { get; set; }
        public string Status { get; set; }
    }
}
