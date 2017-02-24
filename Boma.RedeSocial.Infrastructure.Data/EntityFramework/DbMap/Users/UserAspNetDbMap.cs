using System.Data.Entity.ModelConfiguration;
using Boma.RedeSocial.Domain.Users.Entities;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users
{
    public class UserAspNetDbMap: EntityTypeConfiguration<AspNetUser>
    {
        public UserAspNetDbMap()
        {

            HasKey(u => u.Id);
            HasRequired(u => u.User).WithMany().HasForeignKey(t => t.UserId);
            Property(u => u.UserId).HasColumnName("UserId").IsRequired();
            
            Property(u => u.Id).HasColumnName("Id");
            Property(u => u.UserName).HasColumnName("UserName");
            Property(u => u.PasswordHash).HasColumnName("PasswordHash");

            ToTable("AspNetUsers");
        }
    }
}
