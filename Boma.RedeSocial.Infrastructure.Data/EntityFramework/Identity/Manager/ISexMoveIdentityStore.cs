using Boma.RedeSocial.Domain.Users.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager
{
    public interface ISexMoveIdentityStore: IUserStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
    {
        void SetIdentityStoreUser(User user);
        User GetIdentityUserByUserNameAndPassword(string userName, string password);
        User GetAspNetUserByUserNameAndPassword(string userName, string password);
        
    }
}
