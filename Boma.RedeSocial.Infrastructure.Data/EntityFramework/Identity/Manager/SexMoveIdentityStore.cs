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
        private IdentityUser User { get; set; }
        UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(new SexMoveUnitOfWork());
        public SexMoveIdentityStore()
        {
        }


        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
            //var context = userStore.Context as SexMoveUnitOfWork;
            //User = user;
            //SetPasswordHashAsync(user, user.PasswordHash);

            //context.Users.FirstOrDefault(a => a.Id == );


            //context.Users.Add(User);
            //context.Configuration.ValidateOnSaveEnabled = false;
            //return context.SaveChangesAsync();
        }

        public Task DeleteAsync(User user)
        {
            var context = userStore.Context as SexMoveUnitOfWork;
            context.Users.Remove(user);
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();

        }

        public void Dispose() => userStore.Dispose();

        public Task<User> FindByIdAsync(string userId)
        {
            var context = userStore.Context as SexMoveUnitOfWork;
            return context.Users.Where(u => u.Id == Guid.Parse(userId)).FirstOrDefaultAsync();
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var context = userStore.Context as SexMoveUnitOfWork;
            return context.Users.Where(u => u.UserName.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            var task = userStore.HasPasswordAsync(user);
            return task;
        }

        public Task<string> GetPasswordHashAsync(User user)
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

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            var task = GetPasswordHashAsync(passwordHash);
            user.PasswordHash = task.Result;
            return task;
        }

        public Task<string> GetSecurityStampAsync(User user)
        {
            throw new NotImplementedException();
            //var task = userStore.GetSecurityStampAsync(user);
            //SetApplicationUser(user, identityUser);
            //return task;

        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            var task = userStore.SetSecurityStampAsync(user, stamp);
            return task;

        }

        public Task UpdateAsync(User user)
        {
            var context = userStore.Context as SexMoveUnitOfWork;
            context.Users.Attach(user);
            context.Entry(user).State = EntityState.Modified;
            context.Configuration.ValidateOnSaveEnabled = false;
            return context.SaveChangesAsync();

        }

        public IdentityUser GetIdentityUserByUserNameAndPassword(string userName, string password)
        {
            var context = userStore.Context as SexMoveUnitOfWork;
            var passwordHash = GetPasswordHashAsync(password).Result;
            var user = context.Users.FirstOrDefault(p => p.UserName == userName && p.PasswordHash == passwordHash);

            if (user == null) return null;

            return user;
        }

        public User GetAspNetUserByUserNameAndPassword(string userName, string password)
        {
            var context = userStore.Context as SexMoveUnitOfWork;
            var passwordHash = GetPasswordHashAsync(password).Result;
            return context.Users.Where(u => u.UserName.ToLower() == userName.ToLower() && u.PasswordHash == passwordHash).FirstOrDefaultAsync().Result;
        }

        public void SetIdentityStoreUser(User user)
        {
            throw new NotImplementedException();
        }

        User ISexMoveIdentityStore.GetIdentityUserByUserNameAndPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }

    
}
