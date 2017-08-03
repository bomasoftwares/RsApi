using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.Services;
using System;
using System.Web.Http;

namespace Boma.RedeSocial.Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private UserAppService UserAppService {get;}
        public UserController()
        {
            UserAppService = new UserAppService();
        }


        #region CRUD

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

        #endregion
    }
}
