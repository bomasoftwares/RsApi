using System;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System.Linq;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository: CommonRepository<User>, IUserRepository
    {
        public UserRepository(ISexMoveUnitOfWork uow, ISexMoveContext context)
            :base(uow,context)
        {

        }

        public IQueryable<User> QueryWithoutDeleted() => Uow.ApplicationUser.Where(a => a.DeletedAt == null);
        public User Get(Guid id)
            => QueryWithoutDeleted().FirstOrDefault(a => a.Id == id);
        

        public override void Save(User entity)
        {
            entity.UpdatedAt = entity.CreatedAt = DateTime.UtcNow;
            entity.UpdateBy  = entity.CreateBy  = entity.UserName;

            base.Save(entity);
        }

        public User GetByEmail(string email)
                => QueryWithoutDeleted().FirstOrDefault(a => a.Email == email);


    }
}
