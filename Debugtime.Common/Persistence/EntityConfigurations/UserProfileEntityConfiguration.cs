using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class UserProfileEntityConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileEntityConfiguration()
        {
            
            HasKey(up => up.Id);

            Property(ur => ur.FirstName).IsRequired();

            Property(ur => ur.LastName).IsRequired();


            HasOptional(up => up.Links).WithRequired(lk => lk.UserProfile).WillCascadeOnDelete(true);

            HasMany(up => up.Certifications).WithRequired(crt => crt.UserProfile).WillCascadeOnDelete(true);
        }
    }
}
