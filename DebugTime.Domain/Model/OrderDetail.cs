using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebugTime.Domain.Model
{
    public class OrderDetail
    {
        [ForeignKey("Order")]
        public string OrderId { get; set; }

        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal? OrderDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsDeleted { get; set; }
        public Order Order { get; set; }
    }
}
