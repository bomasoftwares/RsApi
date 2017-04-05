using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Interfaces.Repositories
{

    public interface IUserAspNetRepository
    {
        User Get(Guid id);
        AspNetUser GetIdentityUser(Guid id);
        void SavePasswordKey(Guid id, string passwordKey);
        AspNetUser GetIdentityUserByEmail(string email);
        AspNetUser Update(AspNetUser user);
    }
}
