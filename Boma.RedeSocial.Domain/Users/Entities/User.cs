using Boma.RedeSocial.Domain.Common.Entities;
using Boma.RedeSocial.Domain.Common.Enums;
using System;

namespace Boma.RedeSocial.Domain.Users.Entities
{
    public class User: DomainIdentityBase
    {
        public AccountType AccountType { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public string UrlProfilePhoto { get; set; }
        public string PasswordResetKey { get; private set; }

//        public virtual List<Photo> Photos { get; set; }
        

        [Obsolete("EF hidratated", true)] 
        public User()
        {

        }

        public User(string nickName, string email, AccountType accountType, SubscriptionType subscriptionType)
        {
            UserName = nickName;
            Email = email;
            AccountType = accountType;
            SubscriptionType = subscriptionType;
        }

        public void SetPasswordResetKey(string key) => PasswordResetKey = key;

        internal string ResetPassword(User user) => Guid.NewGuid().ToString().ToUpper().Replace("-", "");

        public void GenerateNewId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
