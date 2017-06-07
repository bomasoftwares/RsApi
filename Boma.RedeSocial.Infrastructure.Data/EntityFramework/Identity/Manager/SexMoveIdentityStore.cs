using Boma.RedeSocial.Crosscut.AssertConcern;
using Boma.RedeSocial.Crosscut.Services;
using Boma.RedeSocial.Domain.Users.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager
{
    public class SexMoveIdentityStore : ISexMoveIdentityStore
    {
        private AspNetUser User { get; set; }
        UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(new SexMoveIdentityContext());
        public SexMoveIdentityStore()
        {
        }


        public Task CreateAsync(AspNetUser user)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            User = user;
            SetPasswordHashAsync(user, user.PasswordHash);

            context.Users.FirstOrDefault(a => a.Id == user.Id);


            context.Users.Add(User);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();
        }

        public Task DeleteAsync(AspNetUser user)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            context.Users.Remove(user);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();

        }

        public void Dispose() => userStore.Dispose();

        public Task<AspNetUser> FindByIdAsync(string userId)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            return context.Users.Where(u => u.Id.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
        }

        public Task<AspNetUser> FindByNameAsync(string userName)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            return context.Users.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }

        public Task<bool> HasPasswordAsync(AspNetUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.HasPasswordAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return task;
        }

        public Task<string> GetPasswordHashAsync(AspNetUser user)
        {
            byte[] salt;
            byte[] buffer2;
            if (user.PasswordHash == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(user.PasswordHash, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            var passwordHash = Convert.ToBase64String(dst);
            return Task.FromResult(passwordHash);
        }

        public Task<string> GetPasswordHashAsync(string password)
        {
            AssertConcern.AssertArgumentNotNull(password, "A senha não deve ser nula");
            AssertConcern.AssertArgumentNotEmpty(password, "A senha não deve ser vazia");

            
            var passwordHash = SecurityService.Encrypt(password);
            return Task.FromResult(passwordHash);
        }

        public Task SetPasswordHashAsync(AspNetUser user, string passwordHash)
        {
            var identityUser = ToIdentityUser(user);
            var task = GetPasswordHashAsync(passwordHash);
            identityUser.PasswordHash = task.Result;
            SetApplicationUser(user, identityUser);
            return task;
        }

        public Task<string> GetSecurityStampAsync(AspNetUser user)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.GetSecurityStampAsync(identityUser);
            SetApplicationUser(user, identityUser);
            return task;

        }

        public Task SetSecurityStampAsync(AspNetUser user, string stamp)
        {
            var identityUser = ToIdentityUser(user);
            var task = userStore.SetSecurityStampAsync(identityUser, stamp);
            SetApplicationUser(user, identityUser);
            return task;

        }

        public Task UpdateAsync(AspNetUser user)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();

        }

        private void SetApplicationUser(AspNetUser user, IdentityUser identityUser)
        {
            User.PasswordHash = identityUser.PasswordHash;
            User.SecurityStamp = identityUser.SecurityStamp;
            User.SetId(Guid.Parse(identityUser.Id));
            User.UserName = identityUser.UserName;
        }

        private IdentityUser ToIdentityUser(AspNetUser user)
        {
            return new IdentityUser
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed
            };
        }

        public void SetIdentityStoreUser(AspNetUser user)
        {
            User = user;
        }

        public IdentityUser GetIdentityUserByUserNameAndPassword(string userName, string password)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            var passwordHash = GetPasswordHashAsync(password).Result;
            var user = context.Users.FirstOrDefault(p => p.UserName == userName && p.PasswordHash == passwordHash);

            if (user == null) return null;

            return ToIdentityUser(user);
        }

        public AspNetUser GetAspNetUserByUserNameAndPassword(string userName, string password)
        {
            var context = userStore.Context as SexMoveIdentityContext;
            var passwordHash = GetPasswordHashAsync(password).Result;
            return context.Users.Where(u => u.UserName.ToLower() == userName.ToLower() && u.PasswordHash == passwordHash).FirstOrDefaultAsync().Result;
        }

    }

    
}
