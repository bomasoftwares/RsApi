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
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Crosscut.Services;
using System.Text;
using Boma.RedeSocial.AppService.Users.Commands.Profiles;
using Boma.RedeSocial.AppService.Users.DTOs.Profiles;
using Boma.RedeSocial.Domain.Profiles.Interfaces;
using Boma.RedeSocial.Domain.Profiles.Entities;
using System.Web;
using System.Data.SqlClient;

namespace Boma.RedeSocial.AppService.Users.Services
{
    public class UserAppService : IUserAppService
    {
        private IUserRepository UserRepository { get; }
        private IUserAspNetRepository UserAspNetRepository { get; }
        private IProfileRepository ProfileRepository { get; }
        private IProfilePeopleConfigurationRepository ProfilePeopleConfigurationRepository { get; }
        private ISexMoveIdentityStore UserIdentityStore { get; }
        private IBomaAuditing SexMoveAuditing { get; set; }
        private ISexMoveUnitOfWork Uow { get; set; }
        private IUserService UserService { get; set; }

        private ISexMoveContext SexMoveContext { get; set; }

        public UserAppService(IUserRepository userRepository, IUserAspNetRepository userAspNetRepository, IProfileRepository profileRepository,
            IProfilePeopleConfigurationRepository profilePeopleConfigurationRepository, ISexMoveIdentityStore userIdentityStore, IBomaAuditing sexMoveAuditing,
                ISexMoveUnitOfWork uow, IUserService userService, ISexMoveContext sexMoveContext)
        {
            UserRepository = userRepository;
            UserRepository.SetUserContext(sexMoveContext.UserContext);

            UserAspNetRepository = userAspNetRepository;
            ProfileRepository = profileRepository;
            ProfilePeopleConfigurationRepository = profilePeopleConfigurationRepository;

            UserIdentityStore = userIdentityStore;
            SexMoveAuditing = sexMoveAuditing;
            Uow = uow;
            UserService = userService;
            SexMoveContext = sexMoveContext;

        }

        #region Serviços de usuários

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
                UrlProfilePhoto = user.UrlProfilePhoto
            };
        }

        public UserDetailDTO Get(string name)
        {
            AspNetUser aspNetUser = UserIdentityStore.FindByNameAsync(name).Result;
            if (aspNetUser == null)
                return new UserDetailDTO() { };

            var user = UserRepository.Get(aspNetUser.UserId);

            if (user == null) return new UserDetailDTO() { };

            return new UserDetailDTO()
            {
                AccountType = (int)user.AccountType,
                AccountTypeDescription = user.AccountType.ToString(),
                Email = user.Email,
                Id = user.Id,
                UrlProfilePhoto = user.UrlProfilePhoto,
                UserName = user.UserName
            };
        }

        public User GetDomainUser(string name)
        {
            var aspNetUser = UserIdentityStore.FindByNameAsync(name).Result;
            var user = UserRepository.Get(aspNetUser.UserId);

            return user;
        }

        public Guid Create(NewUserCommand command)
        {
            try
            {
                UserRepository.SetUserContext(command.NickName);
                var existUser = UserRepository.GetByEmail(command.Email);
                if (existUser == null) existUser = UserRepository.GetByUserName(command.NickName);

                AssertConcern.AssertArgumentTrue(existUser == null, "Usuário já cadastrado");

                var domainUser = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = command.NickName,
                    Email = command.Email,
                    AccountType = AccountType.Normal
                };

                UserRepository.Save(domainUser);
                AssertConcern.AssertArgumentNotGuidEmpty(domainUser.Id, "Id do usuário de domínio criado incorretamente");

                var aspNetUser = new AspNetUser()
                {
                    PasswordHash = command.Password,
                    UserName = domainUser.UserName,
                    Email = domainUser.Email,
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

                throw ex;
            }

        }

        public void Update(Guid UserId, UpdateUserCommand command, string userName)
        {
            // ToDo : editar o e-mail e username nas duas tabelas(Users e AspNetUsers)
            var user = UserRepository.Get(UserId);

            AssertConcern.AssertArgumentNotNull(user, "Usuário inexistente");
            AssertConcern.AssertArgumentTrue(user.Id == UserId, "Usuário inválido para atualização");

            if (command.AccountType != -1)
            {
                AssertConcern.AssertArgumentEnumRange((int)command.AccountType, (int)AccountType.Normal, (int)AccountType.Companion, "Opção de conta inválida");
                user.AccountType = (AccountType)command.AccountType.Value;
            }

            if (!string.IsNullOrWhiteSpace(command.UrlProfilePhoto))
                user.UrlProfilePhoto = command.UrlProfilePhoto;

            user.UpdateBy = userName;
            user.UpdatedAt = DateTime.UtcNow;
            UserRepository.Update(user);
            Uow.Commit();

            // SexMoveAuditing.Audit(new AuditCreateCommand("Usuário atualizado", new { User = user}));
        }

        public void ForgotPassword(ForgotPasswordCommand command)
        {
            var user = UserRepository.GetByEmail(command.Email);
            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");

            var identityUser = UserAspNetRepository.GetIdentityUser(user.Id);
            AssertConcern.AssertArgumentNotNull(identityUser, "Usuário Identity não encontrado");

            var passwordKey = UserService.GeneratePasswordKey(identityUser);
            UserAspNetRepository.SavePasswordKey(identityUser.UserId, passwordKey);

            var sB = new StringBuilder();

            sB.Append("Olá, <br/>").AppendLine()
              .Append(" Para recuperar a senha utilize esse código: <br/>").AppendLine()
              .Append($"<p> <strong>{passwordKey}</strong> </p> ").AppendLine()
              .Append("<br/>").AppendLine()
              .Append("Atenciosamente, <br/>").AppendLine()
              .Append("Equipe SexMove");

            var emailService = new EmailService();
            emailService.SendEmail(sB.ToString(), "SexMove - Recuperar senha", identityUser.Email);

            Uow.Commit();
        }

        public string ResetPassword(ResetPasswordCommand command)
        {
            var user = UserAspNetRepository.GetIdentityUserByEmail(command.Email);

            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");
            AssertConcern.AssertArgumentEquals(user.PasswordResetKey, command.PasswordResetKey, "Código inválido para resetar senha");

            var newPassword = $"{DateTime.UtcNow:MMyy}user@#!{DateTime.UtcNow:yydd}"; // Nova senha para acesso''

            UserIdentityStore.SetIdentityStoreUser(user);
            UserIdentityStore.SetPasswordHashAsync(user, newPassword).Wait();
            user.SetPasswordResetKey(null);
            UserAspNetRepository.Update(user);
            Uow.Commit();
            return newPassword;
        }

        public void SetUserContext(string userName) => SexMoveContext.UserContext = userName;

        #endregion

        #region Serviços de perfil de usuários

        public UserProfileDto GetUserProfile(Guid userId)
        {
            return new UserProfileDto()
            {
                Id = userId,
                Genre = 0,
                GenreDescription = "Man",
                MaritalStatus = 0,
                MaritalStatusDescription = "Single",
                ZipCode = "23070400",
                PeopleOne = new PeopleProfileDto()
                {
                    BirthDate = DateTime.Now,
                    Name = "Batman",
                    EyesColor = 0,
                    EyesColorDescription = "Black",
                    HairColor = 1,
                    HairColorDescription = "Black",
                    Height = 183,
                    Weight = 80,
                    ASmoker = false,
                    ADrinker = true
                },
                Summary = @" Uma pessoa que gosta de se aventurar na vida"
            };
        }

        public Guid InsertProfile(NewProfileCommand command)
        {
            try
            {
                var existProfile = ProfileRepository.GetById(command.UserId);
                AssertConcern.AssertArgumentTrue(existProfile == null, "Usuário já possui um perfil");

                var profile = new Profile(command.UserId);
                profile.Id = Guid.NewGuid();
                profile.Genre = (TypePerson)command.Genre;
                profile.MaritalStatus = (MaritalStatus)command.MaritalStatus;
                profile.Summary = command.Summary;
                profile.ZipCode = command.ZipCode;

                if (command.PeopleOne != null)
                {
                    profile.PeopleOneConfiguration = new ProfilePeopleConfiguration()
                    {
                        Id = Guid.NewGuid(),
                        ADrinker = command.PeopleOne.ADrinker,
                        ASmoker = command.PeopleOne.ASmoker,
                        BirthDate = command.PeopleOne.BirthDate,
                        EyeColor = (EyeColor)command.PeopleOne.EyeColor,
                        HairColor = (HairColor)command.PeopleOne.HairColor,
                        Height = command.PeopleOne.Height,
                        Name = command.PeopleOne.Name,
                        Weight = command.PeopleOne.Weight,
                        ProfileId = profile.Id
                    };
                    ProfilePeopleConfigurationRepository.Save(profile.PeopleOneConfiguration);
                }


                if (command.PeopleTwo != null)
                {
                    profile.PeopleTwoConfiguration = new ProfilePeopleConfiguration()
                    {
                        Id = Guid.NewGuid(),
                        ADrinker = command.PeopleTwo.ADrinker,
                        ASmoker = command.PeopleTwo.ASmoker,
                        BirthDate = command.PeopleTwo.BirthDate,
                        EyeColor = (EyeColor)command.PeopleTwo.EyeColor,
                        HairColor = (HairColor)command.PeopleTwo.HairColor,
                        Height = command.PeopleTwo.Height,
                        Name = command.PeopleTwo.Name,
                        Weight = command.PeopleTwo.Weight,
                        ProfileId = profile.Id,

                    };
                    ProfilePeopleConfigurationRepository.Save(profile.PeopleTwoConfiguration);
                }

                ProfileRepository.Save(profile);

                Uow.Commit();
                // SexMoveAuditing.Audit(new AuditCreateCommand("Perfil criado", new { Profile = profile}));
                // SexMoveAuditing.Audit(new AuditCreateCommand("Configuração de perfil criada", new { ProfilePeopleConfiguration = profile.PeopleOneConfiguration }));
                // SexMoveAuditing.Audit(new AuditCreateCommand("Configuração de perfil criada", new { ProfilePeopleConfiguration = profile.PeopleTwoConfiguration }));


                return profile.Id;

            }
            catch (Exception)
            {
                // SexMoveAuditing.AuditError(new AuditErrorCommand(ex.Message, ex));
                throw;
            }

        }

        public void UpdateProfile(Guid UserId, UpdateProfileCommand command, string userName)
        {
            var profile = UserRepository.Get(UserId);

            AssertConcern.AssertArgumentNotNull(profile, "Usuário inexistente");
            AssertConcern.AssertArgumentTrue(profile.Id == UserId, "Usuário inválido para atualização");

            if (command.AccountType != -1)
            {
                AssertConcern.AssertArgumentEnumRange((int)command.AccountType, (int)AccountType.Normal, (int)AccountType.Companion, "Opção de conta inválida");
                profile.AccountType = (AccountType)command.AccountType.Value;
            }

            profile.UpdateBy = userName;
            profile.UpdatedAt = DateTime.UtcNow;
            UserRepository.Update(profile);
            Uow.Commit();
        }

        public ProfileDto GetProfile(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveProfile(Guid userId)
        {
            throw new NotImplementedException();
        }



        #endregion



    }
}
