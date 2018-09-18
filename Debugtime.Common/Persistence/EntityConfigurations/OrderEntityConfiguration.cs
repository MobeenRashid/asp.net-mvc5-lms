using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class OrderEntityConfiguration:EntityTypeConfiguration<Order>
    {
        public OrderEntityConfiguration()
        {
            HasKey(order => order.Id);

            HasRequired(order => order.User).WithMany(user => user.Orders).HasForeignKey(order => order.UserId);

            HasRequired(order => order.Subscription).WithMany(sub => sub.Orders).HasForeignKey(order => order.SubscriptionId);
        }
    }
}
