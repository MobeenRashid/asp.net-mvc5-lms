using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class OrderDetailEntityConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailEntityConfiguration()
        {
            HasKey(od => od.OrderId);
            Property(od => od.Description).IsOptional();
            Property(od => od.OrderDate).IsRequired();

            HasRequired(od=>od.Order).WithOptional(od=>od.OrderDetail).WillCascadeOnDelete(true);
        }
    }
}
