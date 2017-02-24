using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using System;
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
            return UserAppService.Get(Guid.Empty);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("users")]
        public Guid Post([FromBody] NewUserCommand command)
        {
            if (!ModelState.IsValid) throw new Exception("Comando inválido para criação de usúario");

            return UserAppService.Create(command);
        }

        
    }
}
