using Boma.RedeSocial.Domain.Users.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager
{
    public interface ISexMoveIdentityStore: IUserStore<AspNetUser>, IUserPasswordStore<AspNetUser>, IUserSecurityStampStore<AspNetUser>
    {
        void SetIdentityStoreUser(AspNetUser user);
        IdentityUser GetIdentityUserByUserNameAndPassword(string userName, string password);
        AspNetUser GetAspNetUserByUserNameAndPassword(string userName, string password);
        
    }
}
