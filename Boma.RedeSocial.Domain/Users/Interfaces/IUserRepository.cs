using Boma.RedeSocial.Domain.Common.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System;
using System.Linq;

namespace Boma.RedeSocial.Domain.Users.Interfaces
{
    public interface IUserRepository 
    {
        User GetById(Guid id);
        User GetByEmail(string email);
        User GetByUserName(string userName);
        void SavePasswordKey(Guid id, string passwordKey);
        
        IQueryable<User> GetAll();
        void Save(User entity);
        void Update(User entity);
        void Remove(User entity);
    }
}
