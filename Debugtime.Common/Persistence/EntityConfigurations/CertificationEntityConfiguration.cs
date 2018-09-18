using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class CertificationEntityConfiguration:EntityTypeConfiguration<Certification>
    {
        public CertificationEntityConfiguration()
        {
            HasKey(cert => cert.Id);

            HasRequired(cert => cert.UserProfile).WithMany(pf => pf.Certifications).HasForeignKey(cert => cert.ProfileId);
        }
    }
}
