using System;
using System.Collections.Generic;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Model.View
{
    public class CourseListViewModel
    {
        public IList<CourseCardViewModel> StickerViewModel { get; set; }
        public int Take { get; set; }
        public bool HaveMore { get; set; } = true;
    }
}