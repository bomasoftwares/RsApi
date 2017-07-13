using Boma.RedeSocial.Domain.Users.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users
{
    public class UserDbMap: EntityTypeConfiguration<User>
    {
        public UserDbMap()
        {
            HasKey(u => u.Id);

            Property(u => u.AccountType).HasColumnName("AccountType");
            Property(u => u.UrlProfilePhoto).HasColumnName("UrlProfilePhoto");

            Property(u => u.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(u => u.CreatedAt).IsRequired();

            Property(u => u.UpdateBy).HasColumnName("UpdateBy").HasColumnType("varchar").HasMaxLength(100);
            Property(u => u.DeleteBy).HasColumnName("DeleteBy").HasColumnType("varchar").HasMaxLength(100);

            Property(u => u.Email).HasMaxLength(256).HasColumnType("varchar")
            .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IDX_Users_Email", 1) { IsUnique = true }));
            
            Property(u => u.UserName).HasColumnName("UserName").HasColumnType("varchar");
            Property(u => u.PasswordHash).HasColumnName("PasswordHash").HasColumnType("varchar").HasMaxLength(256);
            Property(u => u.SecurityStamp).HasColumnName("SecurityStamp").HasColumnType("varchar").HasMaxLength(256);
            Property(u => u.PhoneNumber).HasColumnName("PhoneNumber").HasColumnType("varchar").HasMaxLength(100);

            ToTable("AspNetUsers");

        }
    }
}
