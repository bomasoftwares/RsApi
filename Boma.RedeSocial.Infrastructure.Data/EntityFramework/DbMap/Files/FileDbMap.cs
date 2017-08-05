using Boma.RedeSocial.Domain.Files.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users
{
    public class FileDbMap: EntityTypeConfiguration<File>
    {
        public FileDbMap()
        {
            HasKey(f => f.Id);

            Property(f => f.ContentType).HasColumnName("ContentType").HasColumnType("varchar").HasMaxLength(200);
            Property(f => f.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(200);
            Property(f => f.ReferenceId).HasColumnName("ReferenceId");
            Property(f => f.Size).HasColumnName("Size");

            ToTable("Files");

        }
    }
}
