using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Boma.Rs.Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public IUserAppService UserAppService { get; set; }
        public ISexMoveIdentityStore SexMoveIdentityStore { get; set; }

        public UserController(IUserAppService userAppService, ISexMoveIdentityStore sexMoveIdentityStore, IBomaAuditing sexMoveAuditing)
        {
            UserAppService = userAppService;
            SexMoveIdentityStore = sexMoveIdentityStore;
        }

        [HttpGet]
        [Route("users")]
        public UserDetailDTO Get()
        {
            return UserAppService.Get(User.Identity.Name);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("users")]
        public Guid Post([FromBody] NewUserCommand command)
        {
            if (!ModelState.IsValid) throw new Exception("Comando inválido para criação de usúario");

            return UserAppService.Create(command);
        }

        [HttpPut]
        [Route("users/{UserId:guid}")]
        public void Put([FromUri] Guid UserId, [FromBody] UpdateUserCommand command)
        {
            UserAppService.Update(UserId, command, User.Identity.Name);
        }

        [HttpPost]
        [Route("account/forgotPassword")]
        public async void ForgotPassword(ForgotPasswordCommand model)
        {
            if (ModelState.IsValid)
            {
                UserAppService.ForgotPassword(model);
                
                return Ok();
            }

            return BadRequest();
        }


    }
}
