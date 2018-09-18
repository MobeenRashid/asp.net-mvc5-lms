using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CertificationPathEntityConfiguration:EntityTypeConfiguration<CertificationPath>
    {
        public CertificationPathEntityConfiguration()
        {
            HasKey(cp => cp.Id);

            HasMany(cp => cp.Cources).WithMany(cor => cor.CertificationPaths).Map(confObj =>
            {
                confObj.MapLeftKey($"{nameof(Course)}RefId");
                confObj.MapRightKey($"{nameof(CertificationPath)}RefId");
                confObj.ToTable($"{nameof(Course)}s{nameof(CertificationPath)}sTable");
            });
        }
    }
}
