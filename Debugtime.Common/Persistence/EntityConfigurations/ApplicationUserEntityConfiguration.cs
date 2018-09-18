using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class ApplicationUserEntityConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserEntityConfiguration()
        {
            HasKey(ur => ur.Id);
            
            Property(ur => ur.Email).IsRequired();
            
            HasRequired(ur=>ur.UserProfile).WithRequiredPrincipal (up=>up.UserAccount).WillCascadeOnDelete(true);

            HasMany(ur => ur.Orders).WithRequired(od => od.User).WillCascadeOnDelete(true);

            HasMany(u=>u.Courses).WithRequired(c=>c.Author).WillCascadeOnDelete(false);

            HasMany(ur => ur.Subscriptions).WithMany(sub => sub.Users).Map(maping =>
            {
                maping.MapLeftKey($"{nameof(ApplicationUser)}RefId");
                maping.MapRightKey($"{nameof(Subscription)}RefId");
            });
        }
    }
}
