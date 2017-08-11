using System;
using System.Collections.Generic;
using Boma.RedeSocial.Domain.Configurations;

namespace Boma.RedeSocial.AppService.Users.Commands.Profiles
{
    public class UpdateProfileCommand
    {
        public Guid UserId { get; set; }
        public string ZipCode { get; set; }
        public string Summary { get; set; }
        public int Genre { get; set; }
        public int MaritalStatus { get; set; }
        public int MaritalStatusInterest { get; set; }
        public int AccountType { get; set; }
        
        public List<Configuration> Interests { get; set; }
        public string Relationships { get; set; }
    }
}
