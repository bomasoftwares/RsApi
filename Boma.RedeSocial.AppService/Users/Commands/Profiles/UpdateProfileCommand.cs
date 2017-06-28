using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boma.RedeSocial.AppService.Users.DTOs.Profiles;

namespace Boma.RedeSocial.AppService.Users.Commands.Profiles
{
    public class UpdateProfileCommand
    {
        public string ZipCode { get; set; }
        public string Summary { get; set; }
        public int Genre { get; set; }
        public int MaritalStatus { get; set; }
        public int? AccountType { get; set; }
        public PeopleProfileConfigurationDto PeopleOne { get; set; }
        public PeopleProfileConfigurationDto PeopleTwo { get; set; }
    }
}
