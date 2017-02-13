using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;

namespace Boma.RedeSocial.Domain.Configurations.Interfaces
{
    public interface IConfigurationRepository
    {
        Configuration Get(string key);
        Configuration Get(ConfigurationType type, string key);
        void Set(Configuration configuration);
    }
}
