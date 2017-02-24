using Boma.RedeSocial.Domain.Interfaces.Repositories;
using System;
using System.Linq;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;

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
            var user = Uow.AspNetUsers.FirstOrDefault();
            return new User()
            {
                //Id = Guid.Parse(user.Id),
                //Email = user.Email,
                //UserName = user.UserName
            };
        }
    }
}
