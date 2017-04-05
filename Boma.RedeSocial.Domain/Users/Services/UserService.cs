using System;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Domain.Users.Interfaces;

namespace Boma.RedeSocial.Domain.Users.Services
{
    public class UserService: IUserService
    {
        public string GeneratePasswordKey(AspNetUser user)
        {
            return user.ResetPassword(user);
        }
    }
}
