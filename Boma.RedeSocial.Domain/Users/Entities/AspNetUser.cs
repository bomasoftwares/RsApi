using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Boma.RedeSocial.Crosscut.AssertConcern;

namespace Boma.RedeSocial.Domain.Users.Entities
{
    public class AspNetUser : IdentityUser, IUser
    {
        public Guid UserId { get; set; }
        public string PasswordResetKey { get; private set; }


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
            AssertConcern.AssertArgumentNotGuidEmpty(id,"ID do Usuário não pode ser nulo");
            this.UserId = id;
            this.Id = id.ToString().ToUpper();
        }

        public void SetPasswordResetKey(string key)
        { 
            PasswordResetKey = key;
        }

        internal string ResetPassword(AspNetUser identityUser)
            => Guid.NewGuid().ToString().ToUpper().Replace("-","");
    }
}
