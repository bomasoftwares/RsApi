using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Interfaces.Repositories
{

    public interface IUserAspNetRepository
    {
        User Get(Guid id);
    }
}
