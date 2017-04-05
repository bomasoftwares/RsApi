using System.Data.Entity.ModelConfiguration;
using Boma.RedeSocial.Domain.Users.Entities;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users
{
    public class UserAspNetDbMap: EntityTypeConfiguration<AspNetUser>
    {
        public UserAspNetDbMap()
        {

            HasKey(u => u.Id);
            HasRequired(u => u.User).WithMany().HasForeignKey(t => t.UserId);
            Property(u => u.UserId).HasColumnName("UserId").IsRequired();
            Property(u => u.UserName).HasColumnName("UserName");
            Property(u => u.PasswordHash).HasColumnName("PasswordHash");
            Property(u => u.Email)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IDX_AspNetUsers_Email", 1) { IsUnique = true }));

            ToTable("AspNetUsers");
        }
    }
}
