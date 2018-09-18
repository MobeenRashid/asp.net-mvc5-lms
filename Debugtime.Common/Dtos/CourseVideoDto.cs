using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Dtos
{
    public class CourseVideoDto
    {
        public string CourseSectionId { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string SectionTitle { get; set; }
        public int Order { get; set; }
        public bool IsFree { get; set; }
        public bool IsDeleted { get; set; }
        public string Duration { get; set; }
    }
}
