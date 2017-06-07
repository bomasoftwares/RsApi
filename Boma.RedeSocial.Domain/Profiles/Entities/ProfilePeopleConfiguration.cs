using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using System;

namespace Boma.RedeSocial.Domain.Profiles.Entities
{
    public class ProfilePeopleConfiguration: DomainEntity
    {
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }

        public HairColor HairColor { get; set; }

        public EyeColor EyeColor { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public bool ASmoker { get; set; }

        public bool ADrinker { get; set; }

        public Guid ProfileId { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
