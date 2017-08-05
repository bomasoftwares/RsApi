using System;
using System.Linq;

namespace Boma.RedeSocial.Domain.Common.Interfaces
{
    public interface ICommonRepository<T>
        where T: class
    {
        T GetById(Guid id);
        IQueryable<T> GetAll();
        void Save(T entity, string createUser);
        void Update(T entity, string updateUser);
        void Remove(T entity, string deleteUser);
    }
}
