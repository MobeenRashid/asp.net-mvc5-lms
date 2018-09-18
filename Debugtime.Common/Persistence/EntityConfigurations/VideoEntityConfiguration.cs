using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class VideoEntityConfiguration:EntityTypeConfiguration<Video>
    {
        public VideoEntityConfiguration()
        {
            HasKey(vd => new { vd.CourseSectionId,vd.SectionTitle,vd.Path  });

            Property(vd => vd.Title).IsRequired();
          
            Property(c => c.Order).IsRequired();
            
            HasRequired(v => v.CourseSection).WithMany(c => c.Videos).HasForeignKey(cc => new { cc.CourseSectionId, cc.SectionTitle }).WillCascadeOnDelete(true);
        }
    }
}
