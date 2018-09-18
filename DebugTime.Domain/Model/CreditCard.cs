using System;

namespace DebugTime.Domain.Model
{
    public class CreditCard
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CardNumber { get; set; }

        public string AccountId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;

        //navigational properties//
        public ApplicationUser UserAccount { get; set; }
    }
}