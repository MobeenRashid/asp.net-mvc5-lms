using System;

namespace DebugTime.Domain.Model
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;


        public ApplicationUser User { get; set; }
        public Subscription Subscription { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}
