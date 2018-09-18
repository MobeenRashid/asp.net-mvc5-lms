using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class QuestionOptionEntityConfiguration : EntityTypeConfiguration<QuestionOption>
    {
        public QuestionOptionEntityConfiguration()
        {
            HasKey(o => new { o.Key, o.QuestionId });
            Property(o => o.Value).IsRequired();
        }
    }
}
