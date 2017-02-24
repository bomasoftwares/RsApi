using Boma.RedeSocial.AppService.Users.Interfaces;
using System;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using Boma.RedeSocial.Crosscut.AssertConcern;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Crosscut.Auditing.Commands;
using Boma.RedeSocial.Domain.Context.Interfaces;

namespace Boma.RedeSocial.AppService.Users.Services
{
    public class UserAppService : IUserAppService
    {
        private IUserRepository UserRepository { get; }
        private IUserAspNetRepository UserAspNetRepository { get; }
        private ISexMoveIdentityStore UserIdentityStore { get; }
        private IBomaAuditing SexMoveAuditing { get; set; }
        private ISexMoveUnitOfWork Uow { get; set; }


        public UserAppService(IUserRepository userRepository, IUserAspNetRepository userAspNetRepository, ISexMoveIdentityStore userIdentityStore, IBomaAuditing sexMoveAuditing, ISexMoveUnitOfWork uow)
        {
            UserRepository = userRepository;
            UserAspNetRepository = userAspNetRepository;
            UserIdentityStore = userIdentityStore;
            SexMoveAuditing = sexMoveAuditing;
            Uow = uow;
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

        public Guid Create(NewUserCommand command)
        {
            try
            {
                var domainUser = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = command.UserName,
                    Email = command.Email,
                    AccountType = Domain.Common.Enums.AccountType.Normal
                };

                UserRepository.Save(domainUser);

                AssertConcern.AssertArgumentNotGuidEmpty(domainUser.Id, "Id do usuário de domínio criado incorretamente");

                var aspNetUser = new AspNetUser()
                {
                    PasswordHash = command.Password,
                    UserName = domainUser.UserName,
                    UserId = domainUser.Id
                };
                aspNetUser.SetId(domainUser.Id);

                AssertConcern.AssertArgumentNotGuidEmpty(aspNetUser.UserId, "Id do usuário está inválido");
                AssertConcern.AssertArgumentEquals(Guid.Parse(aspNetUser.Id), aspNetUser.UserId, "Ids não batem. Devem sempre ser iguais");

                Uow.Commit();

                UserIdentityStore.CreateAsync(aspNetUser).Wait();


                // SexMoveAuditing.Audit(new AuditCreateCommand("Usuário criado", new { User = domainUser, AspNetUser = aspNetUser }));

                return domainUser.Id;
            }
            catch (Exception ex)
            {
                // SexMoveAuditing.AuditError(new AuditErrorCommand(ex.Message, ex));
                throw;
            }
            
        }
    }
}
