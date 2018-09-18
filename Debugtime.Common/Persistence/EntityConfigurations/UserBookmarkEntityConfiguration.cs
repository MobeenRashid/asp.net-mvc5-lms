using DebugTime.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class UserBookmarkEntityConfiguration : EntityTypeConfiguration<Bookmark>
    {
        public UserBookmarkEntityConfiguration()
        {
            HasKey(b => new { b.UserId, b.CourseId });
        }
    }
}
