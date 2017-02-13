using Boma.RedeSocial.Domain.Users;
using System.Data.Entity.ModelConfiguration;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users
{
    public class UserDbMap: EntityTypeConfiguration<User>
    {
        public UserDbMap()
        {
            HasKey(u => u.Id);
            
            Property(u => u.AccountType).HasColumnName("AccountType");
            Property(u => u.ProfilePhoto.Url).HasColumnName("UrlProfilePhoto");

        }
    }
}
