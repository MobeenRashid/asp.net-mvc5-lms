namespace DebugTime.Domain.Model
{
    public class Bookmark
    {
        public string UserId { get; set; }
        public string CourseId { get; set; }
        public ApplicationUser User { get; set; }
        public Course Course { get; set; }
    }
}
