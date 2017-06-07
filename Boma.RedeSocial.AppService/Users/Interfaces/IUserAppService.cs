using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.Commands.Profiles;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.AppService.Users.DTOs.Profiles;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Domain.Users.Interfaces;
using System;

namespace Boma.RedeSocial.AppService.Users.Interfaces
{
    public interface IUserAppService
    {
        UserDetailDTO Get(string name);
        UserDetailDTO Get(Guid id);

        User GetDomainUser(string name);
        Guid Create(NewUserCommand command);
        void Update(Guid userId, UpdateUserCommand command, string userName);
        void ForgotPassword(ForgotPasswordCommand command);
        string ResetPassword(ResetPasswordCommand command);
        UserProfileDto GetUserProfile(Guid userId);

        Guid InsertProfile(NewProfileCommand command);
        void UpdateProfile(UpdateProfileCommand command);
        ProfileDto GetProfile(Guid userId);
        void RemoveProfile(Guid userId);

        void SetUserContext(string user);
    }
}
