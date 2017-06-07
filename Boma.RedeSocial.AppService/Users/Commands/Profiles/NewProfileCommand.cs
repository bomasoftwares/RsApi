using Boma.RedeSocial.AppService.Users.DTOs.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.AppService.Users.Commands.Profiles
{
    public class NewProfileCommand
    {
        public Guid UserId { get; set; }
        public string ZipCode { get; set; }
        public string Summary { get; set; }
        public int Genre { get; set; }
        public int MaritalStatus { get; set; }

        public PeopleProfileConfigurationDto PeopleOne {get;set;}
        public PeopleProfileConfigurationDto PeopleTwo { get;set;}

    }
}
