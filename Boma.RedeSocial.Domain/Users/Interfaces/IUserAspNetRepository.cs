using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users;
using System;

namespace Boma.RedeSocial.Domain.Interfaces.Repositories
{

    public interface IUserAspNetRepository
    {
        User Get(Guid id);
    }
}
