using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.Commands.Profiles;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Boma.Rs.Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public IUserAppService UserAppService { get; set; }
        public ISexMoveIdentityStore SexMoveIdentityStore { get; set; }

        public UserController(IUserAppService userAppService, ISexMoveIdentityStore sexMoveIdentityStore)
        {
            UserAppService = userAppService;
            SexMoveIdentityStore = sexMoveIdentityStore;

        }

        #region Conta do usuário

        [HttpGet]
        [Route("users")]
        public UserDetailDTO Get()
        {
            var userName = User.Identity.Name;
            UserAppService.SetUserContext(userName);
            return UserAppService.Get(userName);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("users")]
        public Guid Post([FromBody] NewUserCommand command)
        {
            if (!ModelState.IsValid)  throw new Exception("Comando inválido para criação de usúario");
            return UserAppService.Create(command);
        }

        [HttpPut]
        [Route("users/{UserId:guid}")]
        public void Put([FromUri] Guid UserId, [FromBody] UpdateUserCommand command)
        {
            UserAppService.Update(UserId, command, User.Identity.Name);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("users/forgotPassword")]
        public void ForgotPassword(ForgotPasswordCommand command)
        {
            if (ModelState.IsValid)
                UserAppService.ForgotPassword(command);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("users/resetPassword")]
        public string ResetPassword(ResetPasswordCommand command)
        {
            if (ModelState.IsValid)
                return UserAppService.ResetPassword(command);

            return null;
        }

        #endregion


        #region Profile

        [HttpGet]
        [Route("users/{UserId:guid}/profile")]
        public UserProfileDto GetUserProfile([FromUri] Guid userId)
            => UserAppService.GetUserProfile(userId);

        [HttpPost]
        [Route("users/{userId:guid}/profile")]
        public Guid CreateProfile([FromBody]NewProfileCommand command, [FromUri]Guid userId)
        {
            if (!ModelState.IsValid)
                throw new Exception("Comando inválido para criação de um perfil");

            command.UserId = userId;

            return UserAppService.InsertProfile(command);

        }

        #endregion




    }
}
