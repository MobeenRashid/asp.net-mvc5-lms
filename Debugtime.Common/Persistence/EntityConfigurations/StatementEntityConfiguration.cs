using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class StatementEntityConfiguration:EntityTypeConfiguration<Statement>
    {
        public StatementEntityConfiguration()
        {
            HasKey(s => s.TransactionId);

            Property(s => s.Amount).IsRequired();
            Property(s => s.Time).IsRequired();
        }
    }
}
