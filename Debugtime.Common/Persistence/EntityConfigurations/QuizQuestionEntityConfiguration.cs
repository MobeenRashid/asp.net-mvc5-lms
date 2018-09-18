using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class QuizQuestionEntityConfiguration : EntityTypeConfiguration<QuizQuestion>
    {
        public QuizQuestionEntityConfiguration()
        {
            HasKey(qq => new { qq.Key, qq.QuizId });
            
            Property(q => q.Answer).IsRequired();

            Property(q => q.Order).IsRequired();

            HasMany(q => q.QuestionOptions).WithRequired(q => q.Question).HasForeignKey(o => new { o.QuestionKey, o.QuestionId }).WillCascadeOnDelete(true);
        }
    }
}
