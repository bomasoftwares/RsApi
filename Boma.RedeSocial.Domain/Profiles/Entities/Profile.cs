using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Domain.Profiles.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Profiles.Entities
{
    public class Profile : DomainEntity, IProfile
    {
        public string ZipCode { get; set; }

        public string Summary { get; set; }

        public string Resume { get; set; }

        public TypePerson Genre { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        public MaritalStatus MaritalInterest { get; set; }

        public string StateInterest { get; set; } //Todo: Damasio,change this property to one class with states previosly saved in database

        public string CityInterest { get; set; }

        public string UserId { get; set; }

        public ProfilePeopleConfiguration PeopleOneConfiguration { get; set; }

        public ProfilePeopleConfiguration PeopleTwoConfiguration { get; set; }

        public virtual User User { get; set; }

        protected Profile()
        {

        }

        public Profile(Guid userId)
        {
          //  UserId = userId;
        }

    }
}
