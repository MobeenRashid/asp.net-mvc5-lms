using System.Collections.Generic;

namespace DebugTime.Domain.Model
{
    public class CourseSection
    {
        public string CourseId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public Course Course { get; set; }
        public ICollection<Video> Videos { get; set; }
    }
}
