using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class UserCourseProgressEntityConfiguration:EntityTypeConfiguration<UserCourseProgress>
    {
        public UserCourseProgressEntityConfiguration()
        {
            HasKey(up => new{up.UserId,up.CourseId});
        } 
    }
}