using Boma.RedeSocial.Domain.Common.Enums;

namespace Boma.RedeSocial.Domain.Configurations.Interfaces
{
    interface IConfiguration
    {
        ConfigurationType Type { get; set; }
        string Key { get; set; }
        string Value { get; set; }
    }
}
