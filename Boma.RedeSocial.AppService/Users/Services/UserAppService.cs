using System;
using Boma.RedeSocial.AppService.Users.DTOs;
using Boma.RedeSocial.Crosscut.AssertConcern;
using Boma.RedeSocial.AppService.Users.Commands;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Crosscut.Services;
using System.Text;
using Boma.RedeSocial.AppService.Users.Commands.Profiles;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Domain.Users.Services;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Profiles;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Configurations;
using System.Threading.Tasks;
using Boma.RedeSocial.AppService.Users.Profiles.DTOs;
using Boma.RedeSocial.Domain.Profiles.Entities;
using Boma.RedeSocial.Domain.Configurations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Boma.RedeSocial.AppService.Users.Services
{
    public class UserAppService 
    {
        private UserRepository UserRepository { get; }
        private ProfileRepository ProfileRepository { get; }
        private ConfigurationRepository ConfigurationRepository { get; }
        private SexMoveContext Uow { get; set; }
        private UserService UserService { get; set; }

        public UserAppService()
        {
            Uow = new SexMoveContext();
            UserRepository = new UserRepository(Uow);
            UserService = new UserService();
            ProfileRepository = new ProfileRepository(Uow);
            ConfigurationRepository = new ConfigurationRepository(Uow);
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
            if (user == null) return new UserDetailDTO();

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
            var user = UserRepository.GetByUserName(name);
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

        public User GetDomainUserByEmail(string email) => UserRepository.GetByEmail(email);


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

        public void ResetPassword(ResetPasswordCommand command)
        {
            var user = UserRepository.GetByEmail(command.Email);

            AssertConcern.AssertArgumentNotNull(user, "Usuário não encontrado");
            AssertConcern.AssertArgumentEquals(user.PasswordResetKey, command.PasswordResetKey, "Código inválido para resetar senha");
            AssertConcern.AssertArgumentNotNull(command.NewPassword, "A nova senha não pode estar nula");
            AssertConcern.AssertArgumentNotEmpty(command.NewPassword, "A nova senha não pode ser vazio");

            var newPassword = SecurityService.Encrypt(command.NewPassword); 

            user.SetPasswordResetKey(newPassword);
            user.PasswordHash = newPassword;

            UserRepository.Update(user);
            Uow.Commit();
        }

        #endregion

        #region Serviços de perfil de usuários

        public ProfileDto GetUserProfile(Guid userId)
        {
            var profile = ProfileRepository.GetById(userId);
            AssertConcern.AssertArgumentNotNull(profile.UserId, "Usuário não encontrado");

            var returnProfile = new ProfileDto()
            {
                Id = profile.Id,
                Genre = (int)profile.Genre,
                GenreDescription = profile.Genre.ToString(),
                MaritalStatus = (int)profile.MaritalStatus,
                MaritalStatusDescription = profile.MaritalStatus.ToString(),
                ZipCode = profile.ZipCode,
                MaritalStatusInterest = (int)profile.MaritalStatusInterest,
                MaritalStatusInterestDescription = profile.MaritalStatusInterest.ToString(),
                Summary = profile.Summary
            };

            var interestConfigurations = ConfigurationRepository.GetByQuery(profile.UserId,"Interest");
            var relationshipConfigurations = ConfigurationRepository.GetByQuery(profile.UserId, "Relationship");

            Parallel.ForEach(interestConfigurations, x => returnProfile.Interests.Add(x));
            Parallel.ForEach(relationshipConfigurations, x => returnProfile.Relationships.Add(x));

            return returnProfile;


        }
        public void UpdateProfile(Guid UserId, UpdateProfileCommand command, string userName)
        {
            AssertConcern.AssertArgumentNotGuidEmpty(UserId, "Usuário inválido");

            var profile = ProfileRepository.GetById(UserId);
            if (profile == null)
            {
                var genre = (TypePerson)command.Genre;
                var newProfile = new Profile(UserId.ToString(), genre);
                newProfile.GenerateNewId();
                newProfile.Genre = genre;
                newProfile.MaritalStatus = (MaritalStatus)command.MaritalStatus;
                newProfile.ZipCode = command.ZipCode;
                newProfile.MaritalStatusInterest = (MaritalStatus)command.MaritalStatusInterest;
                newProfile.Summary = command.Summary;

                //var interests = JsonConvert.DeserializeObject<List<Configuration>>(command.Interests);
                //var relationships = JsonConvert.DeserializeObject<List<Configuration>>(command.Relationships);
                ProfileRepository.Save(newProfile, userName);

                Parallel.ForEach(command.Interests, x =>
                {
                    var configuration = ConfigurationRepository.Get(x.UserId, x.Key);
                    if (configuration == null)
                        ConfigurationRepository.Create(x);
                    else
                    {
                        configuration.Value = x.Value;
                        ConfigurationRepository.Update(configuration);
                    }
                });

                //Parallel.ForEach(relat, x =>
                //{
                //    var configuration = ConfigurationRepository.Get(x.UserId, x.Key);
                //    if (configuration == null)
                //        ConfigurationRepository.Create(x);
                //    else
                //    {
                //        configuration.Value = x.Value;
                //        ConfigurationRepository.Update(configuration);
                //    }
                //});

            }
            else
            {
                AssertConcern.AssertArgumentNotNull(profile, "Perfil não encontrado");
                AssertConcern.AssertArgumentNotNull(profile.UserId, "Usuário não encontrado");
                profile.Genre = (TypePerson)command.Genre;
                profile.MaritalStatus = (MaritalStatus)command.MaritalStatus;
                profile.ZipCode = profile.ZipCode;
                profile.MaritalStatusInterest = (MaritalStatus)command.MaritalStatusInterest;
                profile.Summary = profile.Summary;
                ProfileRepository.Update(profile, userName);
            }
            
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
