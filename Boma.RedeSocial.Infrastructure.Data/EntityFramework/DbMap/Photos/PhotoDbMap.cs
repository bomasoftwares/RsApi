using Boma.RedeSocial.Domain.Files.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Photos
{
    public class PhotoDbMap: EntityTypeConfiguration<Photo>
    {
        public PhotoDbMap()
        {
            HasKey(p => p.Id);
            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Url).HasColumnName("Url");
            Property(p => p.UploadedAt).HasColumnName("UploadedAt");

            ToTable("Photos");

        }
    }
}
