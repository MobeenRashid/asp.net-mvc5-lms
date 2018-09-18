using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CreditCardEntityConfiguration:EntityTypeConfiguration<CreditCard>
    {
        public CreditCardEntityConfiguration()
        {
            HasKey(cc => cc.Id);

            HasRequired(cc => cc.UserAccount).WithMany(uc => uc.CreditCards).HasForeignKey(cc => cc.AccountId).WillCascadeOnDelete(true);
        }
    }
}
