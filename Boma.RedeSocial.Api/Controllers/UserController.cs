using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.AppService.Users.Services;
using System;
using System.Web.Http;
using Boma.RedeSocial.AppService.Users.Commands.Profiles;
using Boma.RedeSocial.AppService.Users.Profiles.DTOs;

namespace Boma.RedeSocial.Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private UserAppService UserAppService { get; }
        public UserController()
        {
            UserAppService = new UserAppService();
        }


        #region Users

        [HttpPost]
        [AllowAnonymous]
        [Route("users")]
        public IHttpActionResult CreateNewUser(NewUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos para criação da conta");

            var userId = UserAppService.Create(command);

            return Ok(userId);
        }


        [HttpGet]
        [Route("users")]
        public UserDetailDTO GetUserDetails()
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);
            if (user == null) return new UserDetailDTO();

            return UserAppService.Get(user.UserId);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("users/forgotPassword")]
        public void ForgotPassword(ForgotPasswordCommand command)
        {
            UserAppService.ForgotPassword(command);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("users/resetPassword")]
        public void ResetPassword(ResetPasswordCommand command)
        {
            UserAppService.ResetPassword(command);
        }


        #endregion

        #region Profiles
        [HttpPut]
        [Route("users/updateProfile")]
        public void UpdateProfile(UpdateProfileCommand command)
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);
            UserAppService.UpdateProfile(user.UserId, command, User.Identity.Name);
        }
        
        [HttpGet]
        [Route("users/profile")]
        public ProfileDto GetProfile()
        {
            var user = UserAppService.GetDomainUserByEmail(User.Identity.Name);

            return UserAppService.GetUserProfile(user.UserId);
        }
        #endregion
    }
}
