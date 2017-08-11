using Boma.RedeSocial.Domain.Configurations;
using System;
using System.Collections.Generic;

namespace Boma.RedeSocial.AppService.Users.Profiles.DTOs
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string ZipCode { get; set; }
        public string Summary { get; set; }
        public int? Genre { get; set; }
        public string GenreDescription { get; set; }
        public int? MaritalStatus { get; set; }
        public string MaritalStatusDescription { get; set; }
        public int? MaritalStatusInterest { get; set; }
        public string MaritalStatusInterestDescription { get; set; }

        public List<Configuration> Interests { get; set; }
        public List<Configuration> Relationships { get; set; }
    }
  
}
