using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugTime.Domain.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public string BuyerId { get; set; }
        public string CourseId { get; set; }
        public string Order { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public Statement Statement { get; set; }
        public ApplicationUser Buyer { get; set; }
        public Course Course { get; set; }
    }
}
