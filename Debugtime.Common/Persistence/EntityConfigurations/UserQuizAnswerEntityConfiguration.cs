using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class UserQuizAnswerEntityConfiguration : EntityTypeConfiguration<UserQuizAnswer>
    {
        public UserQuizAnswerEntityConfiguration()
        {
            HasKey(q => new { q.UserId, q.QuizId, q.QuestionKey });
            Property(q => q.Answer).IsRequired();
            Property(q => q.AnswerKey).IsRequired();
            Property(q => q.IsCorrect).IsRequired();
        }
    }
}
