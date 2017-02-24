using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.DTOs;
using System;

namespace Boma.RedeSocial.AppService.Users.Interfaces
{
    public interface IUserAppService
    {
        UserDetailDTO Get(Guid id);
        Guid Create(NewUserCommand command);
    }
}
