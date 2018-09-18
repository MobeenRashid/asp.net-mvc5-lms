using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class UserCourseEntityConfiguration:EntityTypeConfiguration<UserCourse>
    {
        public UserCourseEntityConfiguration()
        {
            HasKey(c => new {c.UserId, c.CourseId});
        }
    }
}
