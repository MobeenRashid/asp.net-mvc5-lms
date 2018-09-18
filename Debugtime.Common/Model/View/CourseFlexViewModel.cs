using System.Collections.Generic;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Model.View
{
    public class CourseFlexViewModel
    {
        public CourseFlexViewModel()
        {
            CourseReviews = new HashSet<CourseReview>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public string ThumbnailPath { get; set; }


        public ApplicationUser Author { get; set; }
        public IEnumerable<CourseReview> CourseReviews { get; set; }

    }
}