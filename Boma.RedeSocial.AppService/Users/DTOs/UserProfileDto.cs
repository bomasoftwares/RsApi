using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boma.RedeSocial.AppService.Users.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public int Genre { get; set; }
        public string GenreDescription { get; set; }
        public int MaritalStatus { get; set; }
        public string MaritalStatusDescription { get; set; }
        public string ZipCode { get; set; }
        public string Summary { get; set; }

        public PeopleProfileDto PeopleOne { get; set; }
        public PeopleProfileDto PeopleTwo { get; set; }
    }

    public class PeopleProfileDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int HairColor { get; set; }
        public string HairColorDescription { get; set; }
        public int EyesColor { get; set; }
        public string EyesColorDescription { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public bool ASmoker { get; set; }
        public bool ADrinker { get; set; }
    }

}
