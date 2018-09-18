using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class QuizEntityConfiguration : EntityTypeConfiguration<Quiz>
    {
        public QuizEntityConfiguration()
        {
            HasKey(q => q.QuizId);
            
            HasRequired(q => q.Course).WithOptional(c => c.Quiz).WillCascadeOnDelete(true);

            HasMany(q => q.Questions).WithRequired(q => q.Quiz).HasForeignKey(q => q.QuizId).WillCascadeOnDelete(true);

            HasMany(q => q.Candidates).WithMany(u => u.Assesments).Map(m=>m.ToTable("QuizesCandidates"));
        }
    }
}
