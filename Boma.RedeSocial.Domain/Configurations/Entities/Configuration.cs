using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Configurations
{
    public class Configuration
    {
        public Configuration(string userId, string key, string value)
        {
            UserId = userId;
            Key = key;
            Value = value;
        }

        public string UserId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
