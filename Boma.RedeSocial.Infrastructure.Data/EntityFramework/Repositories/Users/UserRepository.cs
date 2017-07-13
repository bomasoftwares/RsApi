using System;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System.Linq;
using System.Data.Entity;
using Boma.RedeSocial.Domain.Common.Interfaces;
using System.Data;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users
{
    public class UserRepository: IUserRepository
    {
        protected ISexMoveContext SexMoveContext { get; set; }
        protected SexMoveUnitOfWork Uow { get; set; }
        protected IDbConnection Dapper { get; set; }

        public UserRepository(ISexMoveUnitOfWork uow, ISexMoveContext sexMoveContext)
        {
            SexMoveContext = sexMoveContext;
            var _uow = uow as SexMoveUnitOfWork;

            if (_uow == null)
                throw new Exception($"O repositório deve ser do tipo {nameof(SexMoveUnitOfWork)}.");

            Uow = _uow;
            Dapper = _uow.Database.Connection;
        }

        public virtual DbSet<User> CurrentSet() => Uow.Users;
        public virtual IQueryable<User> BaseQuery() => CurrentSet().AsQueryable();
        public IQueryable<User> QueryWithoutDeleted() => CurrentSet().Where(a => a.DeletedAt == null);
        public User GetById(Guid id) => QueryWithoutDeleted().FirstOrDefault(a => a.Id == id);
        public void Save(User entity)
        {
            entity.UpdateBy = entity.CreatedBy = SexMoveContext.UserContext;
            entity.UpdatedAt = entity.CreatedAt = DateTime.UtcNow;

            CurrentSet().Add(entity);
            Uow.SaveChanges();
        }
        public User GetByEmail(string email) => QueryWithoutDeleted().FirstOrDefault(a => a.Email == email);
        public void SetUserContext(string userName) => SexMoveContext.UserContext = userName;
        public User GetByUserName(string userName) => QueryWithoutDeleted().FirstOrDefault(a => a.UserName == userName);
        public void SavePasswordKey(Guid id, string passwordKey)
        {
            var user = Uow.Users.FirstOrDefault(a => a.Id == id);
            user.SetPasswordResetKey(passwordKey);

            Uow.Entry(user).State = EntityState.Modified;
        }

        public IQueryable<User> GetAll() => QueryWithoutDeleted();

        public void Remove(User entity)
        {
            entity.DeleteBy = SexMoveContext.UserContext;
            entity.DeletedAt = DateTime.UtcNow;

            Uow.Entry(entity).State = EntityState.Modified;
        }

        public void Update(User entity) => Uow.Entry(entity).State = EntityState.Modified;
    }
}
