using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CourseReviewEntityConfiguration : EntityTypeConfiguration<CourseReview>
    {
        public CourseReviewEntityConfiguration()
        {
            HasKey(cc => new { cc.CourseId, cc.UserId });
            Property(cc => cc.Stars).IsRequired();
        }
    }
}
