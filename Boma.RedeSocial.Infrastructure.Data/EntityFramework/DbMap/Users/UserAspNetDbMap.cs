using System.Data.Entity.ModelConfiguration;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.DbModel;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users
{
    public class UserAspNetDbMap: EntityTypeConfiguration<AspNetUserDbModel>
    {
        public UserAspNetDbMap()
        {
            HasKey(u => u.Id);

            Property(u => u.Id).HasColumnName("Id");
            Property(u => u.UserName).HasColumnName("UserName");
            Property(u => u.Email).HasColumnName("Email");

            ToTable("AspNetUsers");
        }
    }
}
