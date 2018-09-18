using System.Collections.Generic;

namespace Debugtime.Common.Model.View
{
    public class CourseDashboardViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string ProgrammingLanguege { get; set; }

        public IEnumerable<UserCourseProgressViewModel> UserCourseProgresses { get; set; }
    }
}