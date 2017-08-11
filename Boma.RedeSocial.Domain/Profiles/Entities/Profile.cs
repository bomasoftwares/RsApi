using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Domain.Users.Entities;
using System;

namespace Boma.RedeSocial.Domain.Profiles.Entities
{
    public class Profile : DomainEntity
    {
        [Obsolete("EF Hidratated", true)]
        public Profile()
        {
        }

        public Profile(string userId, TypePerson genre)
        {
            UserId = userId;
            Genre = genre;
        }

        public string ZipCode { get; set; }
        public string Summary { get; set; }
        public TypePerson Genre { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public MaritalStatus MaritalStatusInterest { get; set; }

        public virtual User User { get; set; }
        public string UserId { get; set; }

    }
}
