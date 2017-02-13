using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Domain.Files.Entities;
using System.Collections.Generic;

namespace Boma.RedeSocial.Domain.Interfaces.Entities
{
    public interface IUser
    {
        string UserName { get; set; }
        string Email { get; set; }
        AccountType AccountType { get; set; }
        SubscriptionType SubscriptionType { get; set; }
        Photo ProfilePhoto { get; set; }

        List<Photo> Photos { get; set; }
        List<Video> Videos { get; set; }
    }
}
