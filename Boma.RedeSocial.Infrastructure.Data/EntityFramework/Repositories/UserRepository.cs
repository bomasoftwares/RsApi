using System;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository: CommonRepository<User>, IUserRepository
    {
        public UserRepository(ISexMoveUnitOfWork uow, ISexMoveContext context)
            :base(uow,context)
        {

        }

        public User Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public override void Save(User entity)
        {
            entity.UpdatedAt = entity.CreatedAt = DateTime.UtcNow;
            entity.UpdateBy  = entity.CreateBy  = entity.UserName;

            base.Save(entity);
        }


    }
}
