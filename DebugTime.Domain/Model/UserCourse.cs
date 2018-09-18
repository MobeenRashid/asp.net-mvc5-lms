using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugTime.Domain.Model
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public string CourseId { get; set; }

        public DateTime Date { get; set; }=DateTime.Now;

        public ApplicationUser User { get; set; }
        public Course Course { get; set; }
    }
}
