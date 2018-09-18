namespace DebugTime.Domain.Model
{
    public class CourseReview
    {
        public string CourseId { get; set; }
        public string UserId { get; set; }

        public int Stars { get; set; }
        public string Review { get; set; }

        public Course Course { get; set; }
        public ApplicationUser User { get; set; }
    }
}