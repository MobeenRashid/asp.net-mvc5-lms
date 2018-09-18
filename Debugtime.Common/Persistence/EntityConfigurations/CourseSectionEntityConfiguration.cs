using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CourseSectionEntityConfiguration : EntityTypeConfiguration<CourseSection>
    {
        public CourseSectionEntityConfiguration()
        {
            HasKey(cc => new { cc.CourseId, cc.Title });

            Property(cc => cc.Order).IsRequired();

            HasMany(cc => cc.Videos).WithRequired(c => c.CourseSection).HasForeignKey(cc => new { cc.CourseSectionId, cc.SectionTitle }).WillCascadeOnDelete(true);
        }
    }
}
