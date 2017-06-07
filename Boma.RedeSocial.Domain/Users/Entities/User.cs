using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using Boma.RedeSocial.Domain.Files.Entities;
using Boma.RedeSocial.Domain.Users.Interfaces;
using System.Collections.Generic;

namespace Boma.RedeSocial.Domain.Users.Entities
{
    public class User : DomainEntity, IUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public string UrlProfilePhoto { get; set; }

        public virtual List<Photo> Photos { get; set; }
        

        public User()
        {

        }

        public User(string userName, string email, AccountType accountType, SubscriptionType subscriptionType)
        {
            UserName = userName;
            Email = email;
            AccountType = accountType;
            SubscriptionType = subscriptionType;
        }
        
    }
}
