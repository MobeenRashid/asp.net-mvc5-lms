using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Model.View
{
    public class CourseReviewViewModel
    {
        public string CourseId { get; set; }
        public string UserId { get; set; }

        public int Stars { get; set; }
        public string Review { get; set; }

        public UserReviewModel User { get; set; }
    }
}
