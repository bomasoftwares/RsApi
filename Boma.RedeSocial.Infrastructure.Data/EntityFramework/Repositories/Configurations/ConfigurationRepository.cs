using Boma.RedeSocial.Domain.Configurations;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Configurations
{
    public class ConfigurationRepository
    {
        protected SexMoveContext Uow { get; set; }

        public ConfigurationRepository(SexMoveContext uow)
        {
            Uow = uow;
        }

        public virtual IQueryable<Configuration> CurrentSet() 
            => Uow.Configurations.AsQueryable();

        public Configuration Get(string userId, string key)
            => CurrentSet().FirstOrDefault(c => c.UserId == userId && c.Key == key);

        public IQueryable<Configuration> GetByQuery(string userId, string query)
            => CurrentSet().Where(c => c.UserId == userId && c.Key.Contains(query));

        public void Create(Configuration configuration)
            => Uow.Configurations.Add(configuration);

        public void Update(Configuration configuration)
            => Uow.Entry(configuration).State = EntityState.Modified;

        public void Remove(Configuration configuration)
            => Uow.Entry(configuration).State = EntityState.Deleted;

    }
}



