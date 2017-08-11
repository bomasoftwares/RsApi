using System;

namespace Boma.RedeSocial.Domain.Configurations.Interfaces
{
    public interface IConfigurationRepository
    {
        Configuration Get(Guid userId, string key);
        void Create(Configuration configuration);
        void Update(Configuration configuration);
    }
}
