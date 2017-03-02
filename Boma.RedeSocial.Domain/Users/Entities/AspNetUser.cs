using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Boma.RedeSocial.Domain.Users.Entities
{
    public class AspNetUser : IdentityUser, IUser
    {
        public Guid UserId { get; set; }
        
        public virtual User User { get; set; }

        public AspNetUser()
        {
            Id = UserId.ToString();
        }

        public AspNetUser(string userName)
        {
            Id = UserId.ToString();
            UserName = userName;
        }

        public void SetId(Guid id)
        {
            this.UserId = id;
            this.Id = id.ToString().ToUpper();
        }
    }



}
