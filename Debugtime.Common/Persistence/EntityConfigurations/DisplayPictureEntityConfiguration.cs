using System.Data.Entity.ModelConfiguration;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Persistence.EntityConfigurations
{
    public class DisplayPictureEntityConfiguration:EntityTypeConfiguration<DisplayPicture>
    {
        public DisplayPictureEntityConfiguration()
        {
            HasKey(dp => dp.Id);
            Property(dp => dp.Content).IsRequired();
            Property(dp => dp.ContentType).IsRequired();

            HasRequired(dp=>dp.Profile).WithMany(pf=>pf.DisplayPictures).HasForeignKey(dp=>dp.ProfileId).WillCascadeOnDelete(true);
        }
    }
}
