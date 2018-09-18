using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class TransactionEntityConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionEntityConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.BuyerId).IsRequired();
            Property(t => t.CourseId).IsRequired();
            Property(t => t.Order).IsRequired();
            Property(t => t.PaymentStatus).IsRequired();

            HasRequired(t => t.Statement).WithRequiredPrincipal(s => s.Transaction).WillCascadeOnDelete(true);
        }
    }
}
