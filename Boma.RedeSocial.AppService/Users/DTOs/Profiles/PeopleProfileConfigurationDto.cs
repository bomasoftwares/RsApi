using System;

namespace Boma.RedeSocial.AppService.Users.DTOs.Profiles
{
    public class PeopleProfileConfigurationDto
    {
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public int HairColor { get; set; }

        public int EyeColor { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public bool ASmoker { get; set; }

        public bool ADrinker { get; set; }

        public Guid ProfileId { get; set; }
        
    }
}
