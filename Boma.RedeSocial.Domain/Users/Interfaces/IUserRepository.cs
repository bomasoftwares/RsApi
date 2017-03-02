using Boma.RedeSocial.Domain.Common.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Users.Interfaces
{
    public interface IUserRepository: ICommonRepository<User>
    {
        User Get(Guid id);
        User GetByEmail(string email);
    }
}
