using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebugTime.Domain.Model
{
    public class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<QuizQuestion>();
        }
        
        public string QuizId { get; set; }
        public string Title { get; set; }

        public Course Course { get; set; }

        public ICollection<QuizQuestion> Questions { get; set; }
        public ICollection<ApplicationUser> Candidates { get; set; }
    }
    
}
