using Boma.RedeSocial.AppService.Users.Interfaces;
using System;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.Crosscut.AssertConcern;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.Domain.Users.Entities;
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
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Domain.Users.Services;

namespace Boma.RedeSocial.AppService.Users.Services
{
    public class UserAppService : IUserAppService
    {
        private UserRepository UserRepository { get; }
        private SexMoveContext Uow { get; set; }
        private UserService UserService { get; set; }

        public UserAppService()
        {
            Uow = new SexMoveContext();
            UserRepository = new UserRepository(Uow);
            UserService = new UserService();
        }

        #region Authenticate

        public User Authenticate(string userName, string password)
        {
            var hashPassword = SecurityService.Encrypt(password);
            return UserRepository.Authenticate(userName, hashPassword);
        }
        

        #endregion

        #region Serviços de usuários

        public UserDetailDTO Get(Guid id)
        {
            var user = UserRepository.GetById(id);

            AssertConcern.AssertArgumentNotNull(user, "Usuário .NET não encontrado");

            return new UserDetailDTO()
            {
                Id = user.UserId,
                Email = user.Email,
                NickName = user.UserName,
                AccountType = (int)user.AccountType,
                AccountTypeDescription = user.AccountType.ToString(),
                UrlProfilePhoto = user.UrlProfilePhoto
            };
        }

        public UserDetailDTO Get(string name)
        {
            User user = UserRepository.GetByUserName(name);
            if (user == null)
                return new UserDetailDTO() { };

            return new UserDetailDTO()
            {
                AccountType = (int)user.AccountType,
                AccountTypeDescription = user.AccountType.ToString(),
                Email = user.Email,
                Id = user.UserId,
                UrlProfilePhoto = user.UrlProfilePhoto,
                NickName = user.UserName
            };
        }

        public User GetDomainUser(string name) => UserRepository.GetByUserName(name);

        public Guid Create(NewUserCommand command)
        {
            try
            {
                var existUser = UserRepository.GetByEmail(command.Email);
                if (existUser == null) existUser = UserRepository.GetByUserName(command.NickName);

                AssertConcern.AssertArgumentTrue(existUser == null, "Usuário já cadastrado");

                var user = new User(command.NickName, command.Email, AccountType.Normal, SubscriptionType.Gratuity);
                user.GenerateNewId();
                user.PasswordHash = SecurityService.Encrypt(command.Password);
                UserRepository.Save(user);
                AssertConcern.AssertArgumentNotGuidEmpty(user.UserId, "Id do usuário de domínio criado incorretamente");

                Uow.Commit();

                return user.UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(Guid UserId, UpdateUserCommand command, string userName)
        {
            // ToDo : editar o e-mail e username nas duas tabelas(Users e AspNetUsers)
            var user = UserRepository.GetById(UserId);

            AssertConcern.AssertArgumentNotNull(user, "Usuário inexistente");
            AssertConcern.AssertArgumentTrue(user.Id == UserId.ToString(), "Usuário inválido para atualização");

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

            var passwordKey = UserService.GeneratePasswordKey(user);
            UserRepository.SavePasswordKey(user.UserId, passwordKey);

            var sB = new StringBuilder();

            sB.Append("Olá, <br/>").AppendLine()
              .Append(" Para recuperar a senha utilize esse código: <br/>").AppendLine()
              .Append($"<p> <strong>{passwordKey}</strong> </p> ").AppendLine()
              .Append("<br/>").AppendLine()
              .Append("Atenciosamente, <br/>").AppendLine()
              .Append("Equipe SexMove");

            var emailService = new EmailService();
            emailService.SendEmail(sB.ToString(), "SexMove - Recuperar senha", user.Email);

            Uow.Commit();
        }

        public string ResetPassword(ResetPasswordCommand command)
        {
            var user = UserRepository.GetByEmail(command.Email);

            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");
            AssertConcern.AssertArgumentEquals(user.PasswordResetKey, command.PasswordResetKey, "Código inválido para resetar senha");

            var newPassword = $"{DateTime.UtcNow:MMyy}user@#!{DateTime.UtcNow:yydd}"; 

            var hashResetKey = SecurityService.Encrypt(newPassword);
            user.SetPasswordResetKey(hashResetKey);
            UserRepository.Update(user);
            Uow.Commit();
            return newPassword;
        }

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
            return Guid.Empty;

            //try
            //{
                //var existProfile = ProfileRepository.GetById(command.UserId);
                //AssertConcern.AssertArgumentTrue(existProfile == null, "Usuário já possui um perfil");

                //var profile = new Profile(command.UserId);
                //profile.Id = Guid.NewGuid();
                //profile.Genre = (TypePerson)command.Genre;
                //profile.MaritalStatus = (MaritalStatus)command.MaritalStatus;
                //profile.Summary = command.Summary;
                //profile.ZipCode = command.ZipCode;

                //if (command.PeopleOne != null)
                //{
                //    profile.PeopleOneConfiguration = new ProfilePeopleConfiguration()
                //    {
                //        Id = Guid.NewGuid(),
                //        ADrinker = command.PeopleOne.ADrinker,
                //        ASmoker = command.PeopleOne.ASmoker,
                //        BirthDate = command.PeopleOne.BirthDate,
                //        EyeColor = (EyeColor)command.PeopleOne.EyeColor,
                //        HairColor = (HairColor)command.PeopleOne.HairColor,
                //        Height = command.PeopleOne.Height,
                //        Name = command.PeopleOne.Name,
                //        Weight = command.PeopleOne.Weight,
                //        ProfileId = profile.Id
                //    };
                //    ProfilePeopleConfigurationRepository.Save(profile.PeopleOneConfiguration);
                //}


                //if (command.PeopleTwo != null)
                //{
                //    profile.PeopleTwoConfiguration = new ProfilePeopleConfiguration()
                //    {
                //        Id = Guid.NewGuid(),
                //        ADrinker = command.PeopleTwo.ADrinker,
                //        ASmoker = command.PeopleTwo.ASmoker,
                //        BirthDate = command.PeopleTwo.BirthDate,
                //        EyeColor = (EyeColor)command.PeopleTwo.EyeColor,
                //        HairColor = (HairColor)command.PeopleTwo.HairColor,
                //        Height = command.PeopleTwo.Height,
                //        Name = command.PeopleTwo.Name,
                //        Weight = command.PeopleTwo.Weight,
                //        ProfileId = profile.Id,

                //    };
                //    ProfilePeopleConfigurationRepository.Save(profile.PeopleTwoConfiguration);
                //}

                //ProfileRepository.Save(profile);

                //Uow.Commit();
                // SexMoveAuditing.Audit(new AuditCreateCommand("Perfil criado", new { Profile = profile}));
                // SexMoveAuditing.Audit(new AuditCreateCommand("Configuração de perfil criada", new { ProfilePeopleConfiguration = profile.PeopleOneConfiguration }));
                // SexMoveAuditing.Audit(new AuditCreateCommand("Configuração de perfil criada", new { ProfilePeopleConfiguration = profile.PeopleTwoConfiguration }));


                //return profile.Id;

            //}
            //catch (Exception)
            //{
            //    // SexMoveAuditing.AuditError(new AuditErrorCommand(ex.Message, ex));
            //    throw;
            //}

        }

        public void UpdateProfile(Guid UserId, UpdateProfileCommand command, string userName)
        {
            var profile = UserRepository.GetById(UserId);

            AssertConcern.AssertArgumentNotNull(profile, "Usuário inexistente");
            AssertConcern.AssertArgumentTrue(profile.Id == UserId.ToString(), "Usuário inválido para atualização");

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
