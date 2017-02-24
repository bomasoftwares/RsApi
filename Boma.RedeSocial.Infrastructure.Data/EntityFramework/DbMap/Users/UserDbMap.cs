using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Users.Entities;
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
            Ignore(p => p.ProfilePhoto);
            Ignore(p => p.Videos);
            Ignore(p => p.Photos);

            ToTable("UserDetails");

        }
    }
}
