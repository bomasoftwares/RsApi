using Boma.RedeSocial.Domain.Interfaces.Repositories;
using System;
using System.Linq;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System.Data.Entity;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserAspNetRepository : IUserAspNetRepository
    {
        protected ISexMoveContext SexMoveContext { get; set; }
        protected SexMoveUnitOfWork Uow { get; set; }

        public UserAspNetRepository(ISexMoveUnitOfWork uow, ISexMoveContext context)
        {
            SexMoveContext = context;

            var _uow = uow as SexMoveUnitOfWork;

            if (_uow == null)
                throw new Exception($"O repositório deve ser do tipo {nameof(SexMoveUnitOfWork)}.");

            Uow = _uow;
        }

        public User Get(Guid id)
        {
            var user = Uow.AspNetUsers.FirstOrDefault(a => a.UserId == id);
            return new User()
            {
                Id = Guid.Parse(user.Id),
                Email = user.Email,
                UserName = user.UserName
            };
        }

        public AspNetUser GetIdentityUser(Guid id)
            => Uow.AspNetUsers.FirstOrDefault(a => a.UserId == id);

        public void SavePasswordKey(Guid id, string passwordKey)
        {
            var user = Uow.AspNetUsers.FirstOrDefault(a => a.UserId == id);
            user.SetPasswordResetKey(passwordKey);

            Uow.Entry(user).State = EntityState.Modified;
        }

        public AspNetUser GetIdentityUserByEmail(string email)
             => Uow.AspNetUsers.FirstOrDefault(a => a.Email == email);

        public AspNetUser Update(AspNetUser user)
        {
            var identityUser = Uow.AspNetUsers.FirstOrDefault(a => a.Id == user.Id);
            identityUser = user;

            Uow.Entry(identityUser).State = EntityState.Modified;
            return identityUser;
        }
    }
}
