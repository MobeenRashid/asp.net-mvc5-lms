using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class LinksEntityConfiguration:EntityTypeConfiguration<Links>
    {
        public LinksEntityConfiguration()
        {
            HasKey(lk => lk.UserProfileId);

            HasRequired(lk => lk.UserProfile).WithOptional(up => up.Links).WillCascadeOnDelete(true);
        }
    }
}
