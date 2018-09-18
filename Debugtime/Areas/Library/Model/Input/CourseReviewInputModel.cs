using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugtime.Areas.Library.Model.Input
{
    public class CourseReviewInputModel
    {
        public string CourseId { get; set; }
        public string UserId { get; set; }
        public string UserReview { get; set; }
        public int UserStars { get; set; }
    }
}