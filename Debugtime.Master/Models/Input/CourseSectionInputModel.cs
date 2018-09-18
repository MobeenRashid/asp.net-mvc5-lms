using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugtime.Master.Models.Input
{
    public class CourseSectionInputModel
    {
        public String CourseId { get; set; }
        public String Title { get; set; }
        public int Order { get; set; }
    }
}