using DebugTime.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CatagoryEntityConfiguration: EntityTypeConfiguration<Catagory>
    {
        public CatagoryEntityConfiguration()
        {
            HasKey(q => q.Id);

            Property(p => p.Title).IsRequired();

        }
    }
}
