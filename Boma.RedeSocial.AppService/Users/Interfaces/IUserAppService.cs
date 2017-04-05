using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.DTOs;
using System;

namespace Boma.RedeSocial.AppService.Users.Interfaces
{
    public interface IUserAppService
    {
        UserDetailDTO Get(string name);
        UserDetailDTO Get(Guid id);
        Guid Create(NewUserCommand command);
        void Update(Guid userId, UpdateUserCommand command, string userName);
        void ForgotPassword(ForgotPasswordCommand command);
        string ResetPassword(ResetPasswordCommand command);
        UserProfileDto GetUserProfile(Guid userId);
    }
}
