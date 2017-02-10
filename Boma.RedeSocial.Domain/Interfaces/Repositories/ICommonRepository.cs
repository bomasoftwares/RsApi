using System;
using System.Linq;

namespace Boma.RedeSocial.Domain.Interfaces.Repositories
{
    public interface ICommonRepository<T>
        where T: class
    {
        T GetById(Guid id);
        IQueryable<T> GetAll();
        void Save(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
