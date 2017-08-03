using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Interfaces;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Domain.Users.Interfaces;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories
{
    public abstract class CommonRepository<T> : ICommonRepository<T>
        where T : DomainEntity
    {
        protected ISexMoveContext SexMoveContext { get; set; }
        protected SexMoveContext Uow { get; set; }
        protected IDbConnection Dapper { get; set; }

        public CommonRepository(ISexMoveUnitOfWork uow, ISexMoveContext sexMoveContext)
        {
            SexMoveContext = sexMoveContext;
            var _uow = uow as SexMoveContext;

            if (_uow == null)
                throw new Exception($"O repositório deve ser do tipo {nameof(Data.SexMoveContext)}.");

            Uow = _uow;
            Dapper = _uow.Database.Connection;
        }

        public virtual IQueryable<T> BaseQuery() => Uow.Set<T>().AsQueryable();

        public IQueryable<T> QueryWithoutDeleted() => Uow.Set<T>().Where(a => a.DeletedAt == null);

        public virtual DbSet<T> CurrentSet() => Uow.Set<T>();


        public IQueryable<T> GetAll()
            => BaseQuery();

        public T GetById(Guid id)
            => BaseQuery().FirstOrDefault(p => p.Id == id);
        

        public void Remove(T entity)
        {
            entity.DeleteBy = SexMoveContext.UserContext;
            entity.DeletedAt = DateTime.UtcNow;

            Uow.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save(T entity)
        {
            entity.UpdateBy  =  entity.CreateBy  = SexMoveContext.UserContext;
            entity.UpdatedAt =  entity.CreatedAt = DateTime.UtcNow;
            
            CurrentSet().Add(entity);
            Uow.SaveChanges();
            
        }
        public void Update(T entity) => Uow.Entry(entity).State = EntityState.Modified;

    }
}
