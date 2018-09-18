using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebugTime.Domain.Model
{
    public class Statement
    {
        [ForeignKey("Transaction")]
        public string TransactionId { get; set; }
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public Transaction Transaction { get; set; }
    }
}