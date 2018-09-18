using System.Collections.Generic;

namespace Debugtime.Common.Model.View
{
    public class CourseSectionViewModel
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }


        public IList<VideoViewModel> Videos { get; set; }
    }
}