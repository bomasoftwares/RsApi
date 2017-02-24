using Boma.RedeSocial.Domain.Users.Entities;
using Microsoft.AspNet.Identity;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager
{
    public interface ISexMoveIdentityStore: IUserStore<AspNetUser>, IUserPasswordStore<AspNetUser>, IUserSecurityStampStore<AspNetUser>
    {
    }
}
