using Boma.RedeSocial.Domain.Common.Interfaces;
using Boma.RedeSocial.Domain.Users;
using System;

namespace Boma.RedeSocial.Domain.Interfaces.Repositories
{
    public interface IUserRepository: ICommonRepository<User>
    {
        User Get(Guid id);
    }
}
