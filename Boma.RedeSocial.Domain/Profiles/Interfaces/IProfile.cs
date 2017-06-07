using Boma.RedeSocial.Domain.Common.Enums;
using System;

namespace Boma.RedeSocial.Domain.Profiles.Interfaces
{
    public interface IProfile
    {
        Guid UserId { get; set; }
        TypePerson Genre { get; set; }
        MaritalStatus MaritalStatus { get; set; }
        string ZipCode { get; set; }
        string Summary { get; set; }
    }
}
