using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Interfaces;
using Boma.RedeSocial.Domain.Interfaces.Entities;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories
{
    public abstract class CommonRepository<T> : ICommonRepository<T>
        where T : DomainEntityBase
    {
        protected ISexMoveContext SexMoveContext { get; set; }
        protected SexMoveUnitOfWork Uow { get; set; }
        protected IDbConnection Dapper { get; set; }

        public CommonRepository(ISexMoveUnitOfWork uow, ISexMoveContext sexMoveContext)
        {
            SexMoveContext = sexMoveContext;
            var _uow = uow as SexMoveUnitOfWork;

            if (_uow == null)
                throw new Exception($"O repositório deve ser do tipo {nameof(SexMoveUnitOfWork)}.");

            Uow = _uow;
            Dapper = _uow.Database.Connection;
        }

        public virtual IQueryable<T> BaseQuery() => Uow.Set<T>().AsQueryable();

        public virtual DbSet<T> CurrentSet() => Uow.Set<T>();


        public IQueryable<T> GetAll()
            => BaseQuery();

        public T GetById(Guid id)
            => BaseQuery().FirstOrDefault(p => p.Id == id);
        

        public void Remove(T entity)
        {
            CurrentSet().Remove(entity);
        }

        public void Save(T entity) => CurrentSet().Add(entity);
        public void Update(T entity) => Uow.Entry(entity).State = EntityState.Modified;

    }
}
