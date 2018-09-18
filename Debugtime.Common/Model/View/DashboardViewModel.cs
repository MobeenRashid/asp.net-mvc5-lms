using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Model.View
{
    public class DashboardViewModel
    {
        public CourseDashboardViewModel InProgress { get; set; }
        public IEnumerable<CourseDashboardViewModel> RecentlyPlayed { get; set; }
        public IEnumerable<CourseCardViewModel> Bookmarks { get; set; }
    }
}
