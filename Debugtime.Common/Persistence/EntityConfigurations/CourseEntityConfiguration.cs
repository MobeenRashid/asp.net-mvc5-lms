using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CourseEntityConfiguration : EntityTypeConfiguration<Course>
    {
        public CourseEntityConfiguration()
        {
            HasKey(cor => cor.Id);

            Property(c => c.Title).IsRequired();
            Property(c => c.ProgrammingLanguege).IsRequired();
            Property(c => c.Description).IsRequired();
            Property(c => c.AuthorId).IsRequired();
            Property(c => c.AgendaItems).IsRequired();


            HasMany(c => c.CourseSections).WithRequired(c=>c.Course).HasForeignKey(c=>c.CourseId).WillCascadeOnDelete(true);
            

            HasOptional(c => c.Quiz).WithRequired(a => a.Course).WillCascadeOnDelete(true);
        }
    }
}
