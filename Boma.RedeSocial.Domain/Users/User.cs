using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Domain.Files;
using System.Collections.Generic;

namespace Boma.RedeSocial.Domain.Users
{
    public class User : DomainEntityBase 
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public Photo ProfilePhoto { get; set; }

        public List<Photo> Photos { get; set; }
        public List<Video> Videos { get; set; }

        public User(string userName, string email, AccountType accountType, SubscriptionType subscriptionType)
        {
            UserName = userName;
            Email = email;
            AccountType = accountType;
            SubscriptionType = subscriptionType;
        }
        
    }
}
