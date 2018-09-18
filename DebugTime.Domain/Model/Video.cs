using System;

namespace DebugTime.Domain.Model
{
    public class Video
    {
        public string CourseSectionId { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public bool IsFree { get; set; }
        public string Duration { get; set; }


        public string SectionTitle { get; set; }
        
        //navigational properties//
        public CourseSection CourseSection { get; set; }
    }
}