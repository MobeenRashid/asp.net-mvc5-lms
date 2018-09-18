using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Model.View
{
    public class CourseSearchViewModel
    {
        public CourseSearchViewModel(int page, int pageSize, int maxPages, int resultCount)
        {
            Courses = new List<CourseFlexViewModel>();
            MaxPages = maxPages;
            Page = page;
            PageSize = pageSize;
            ResultCount = resultCount;
        }

        public int ResultCount { get; set; }

        public int PageSize { get; set; }

        public int NextPage
        {
            get
            {
                var temp = Page;
                return ++temp;
            }
        }

        public int PrevPage
        {
            get
            {
                var temp = Page;
                return --temp;
            }
        }
        public bool HaveMore => End < TotalPages;
        public int MorePages => HaveMore ? TotalPages - Page : 0;
        public int Start { get; set; }
        public int End { get; set; }
        public bool HaveNext => Page < End;
        public bool HavePrev => Page > 1;
        public int TotalPages => (int)Math.Ceiling((decimal)ResultCount / PageSize);
        public int MaxPages { get; set; }
        public List<CourseFlexViewModel> Courses { get; set; }
        public int Page { get; set; }

        public void SetStart()
        {
            if (Page < MaxPages)
            {
                Start = 1;
            }
            else
            {
                if (Page == TotalPages)
                    Start = Page - MaxPages;
                else
                    Start = Page;
            }

            SetEnd();
        }

        public void SetEnd()
        {
            if (Page < MaxPages)
                End = TotalPages < MaxPages ? TotalPages : MaxPages;
            else
            {
                if (Page == TotalPages)
                    End = Page;
                else
                    End = MorePages < MaxPages ? Start + MorePages : Start + MaxPages;
            }
        }
    }
}
