using System;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System.Linq;
using System.Data.Entity;
using System.Data;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users
{
    public class UserRepository: IUserRepository
    {
        public UserRepository(SexMoveContext uow)
        {
            Uow = uow;
        }

        protected SexMoveContext Uow { get; set; }

        public virtual DbSet<User> CurrentSet() => Uow.AppUsers;
        public virtual IQueryable<User> BaseQuery() => CurrentSet().AsQueryable();
        public IQueryable<User> QueryWithoutDeleted() => CurrentSet().Where(a => a.DeletedAt == null);
        public User GetById(Guid id) => QueryWithoutDeleted().FirstOrDefault(a => a.Id == id.ToString());
        public void Save(User entity)
        {
            entity.UpdateBy = entity.CreatedBy = entity.UserName;
            entity.UpdatedAt = entity.CreatedAt = DateTime.UtcNow;

            CurrentSet().Add(entity);
        }

        public User GetByEmail(string email) => QueryWithoutDeleted().FirstOrDefault(a => a.Email == email);
        public User GetByUserName(string userName) => QueryWithoutDeleted().FirstOrDefault(a => a.UserName == userName);
        public void SavePasswordKey(Guid id, string passwordKey)
        {
            var user = Uow.AppUsers.FirstOrDefault(a => a.Id == id.ToString());
            user.SetPasswordResetKey(passwordKey);

            Uow.Entry(user).State = EntityState.Modified;
        }
        public IQueryable<User> GetAll() => QueryWithoutDeleted();
        public void Remove(User entity)
        {
            entity.DeletedAt = DateTime.UtcNow;

            Uow.Entry(entity).State = EntityState.Modified;
        }
        public void Update(User entity) => Uow.Entry(entity).State = EntityState.Modified;

        public User Authenticate(string userName, string password)
        {
            return CurrentSet().FirstOrDefault(a => a.UserName == userName && a.PasswordHash == password);
        }
    }
}
