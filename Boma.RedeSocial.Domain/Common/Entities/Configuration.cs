using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Domain.Configurations.Interfaces;

namespace Boma.RedeSocial.Domain.Common.Entities
{
    public class Configuration: IConfiguration
    {
        public ConfigurationType Type { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        
    }
}
