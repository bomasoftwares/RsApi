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
            Property(u => u.Email)
                .HasMaxLength(256) // Tamanho padrão do Identity
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IDX_Users_Email", 1) { IsUnique = true }));

            //Ignore(p => p.ProfilePhoto);
            //Ignore(p => p.Videos);
            //Ignore(p => p.Photos);

            ToTable("Users");

        }
    }
}
