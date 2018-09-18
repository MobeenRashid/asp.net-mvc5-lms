using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class SubscriptionEntityConfiguration:EntityTypeConfiguration<Subscription>
    {
        public SubscriptionEntityConfiguration()
        {
            HasKey(sub => sub.Id);

            HasMany(sub=>sub.Orders).WithRequired(od=>od.Subscription).WillCascadeOnDelete(false);
        }
    }
}
