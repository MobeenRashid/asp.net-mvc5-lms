using System;
using System.Collections.Generic;

namespace DebugTime.Domain.Model
{
    public class UserCourseProgress
    {
        public string UserId { get; set; }
        public string CourseId { get; set; }

        public int Level { get; set; } = 0;
        public int Lesson { get; set; } = 0;
        public int Progress { get; set; }
        public bool IsRecent { get; set; } = false;

        //navigational property
        public ApplicationUser User { get; set; }
        public Course Course { get; set; }

    }
}
