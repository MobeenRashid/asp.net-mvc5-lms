using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Dtos
{
   public class UsageSymmaryDto
    {
        public int TotalUsers { get; set; }

        public int TotalCourses { get; set; }

        public string UsedMediaSize { get; set; } 
        //comment

        public int BuyCourses { get; set; }

        public List<string> AuthorNames { get; set; }
    }
}
