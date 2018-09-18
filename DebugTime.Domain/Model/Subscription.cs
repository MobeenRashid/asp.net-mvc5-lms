using System;
using System.Collections.Generic;

namespace DebugTime.Domain.Model
{
    public class Subscription
    {
        public Subscription()
        {
            Users= new HashSet<ApplicationUser>();
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();


        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;


        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}