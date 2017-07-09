using System;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System.Linq;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users
{
    public class UserRepository: CommonRepository<User>, IUserRepository
    {
        public UserRepository(ISexMoveUnitOfWork uow, ISexMoveContext context)
            :base(uow,context)
        {
            
        }

        public User Get(Guid id)
            => QueryWithoutDeleted().FirstOrDefault(a => a.Id == id);
        
        public override void Save(User entity)
        {
            base.Save(entity);
        }

        public User GetByEmail(string email)
                => QueryWithoutDeleted().FirstOrDefault(a => a.Email == email);

        public void SetUserContext(string userName) => SexMoveContext.UserContext = userName;

        public User GetByUserName(string userName) 
            => QueryWithoutDeleted().FirstOrDefault(a => a.UserName == userName);



    }
}
