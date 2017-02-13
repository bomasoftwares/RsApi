using Boma.RedeSocial.AppService.Users.Interfaces;
using System;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using Boma.RedeSocial.Crosscut.AssertConcern;

namespace Boma.RedeSocial.AppService.Users.Services
{
    public class UserAppService : IUserAppService
    {
        private IUserRepository UserRepository { get; }
        private IUserAspNetRepository UserAspNetRepository { get; }

        public UserAppService(IUserRepository userRepository, IUserAspNetRepository userAspNetRepository)
        {
            UserRepository = userRepository;
            UserAspNetRepository = userAspNetRepository;
        }

        public UserDetailDTO Get(Guid id)
        {
            var aspNetUser = UserAspNetRepository.Get(id);
            var user = UserRepository.Get(id);

            AssertConcern.AssertArgumentNotNull(aspNetUser, "Usuário .NET não encontrado");
            AssertConcern.AssertArgumentNotNull(user, "Usuário .NET não encontrado");
            AssertConcern.AssertArgumentNotEquals(user.Id, aspNetUser.Id, "Usuário diferentes foram encontrados");

            return new UserDetailDTO()
            {
                Id = aspNetUser.Id,
                Email = aspNetUser.Email,
                UserName = aspNetUser.UserName,
                AccountType = (int)user.AccountType,
                AccountTypeDescription = user.AccountType.ToString(),
                UrlProfilePhoto = user.ProfilePhoto.Url
            };
        }
    }
}
