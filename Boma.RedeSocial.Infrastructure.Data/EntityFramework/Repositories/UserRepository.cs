using System;
using Boma.RedeSocial.Domain.Interfaces;
using Boma.RedeSocial.Domain.Interfaces.Entities;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Context.Interfaces;

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
    }
}
