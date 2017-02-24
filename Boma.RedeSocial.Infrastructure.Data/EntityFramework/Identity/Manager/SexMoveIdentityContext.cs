using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users;
using System.Data.Entity;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager
{
    public class SexMoveIdentityContext : DbContext
    {
        public SexMoveIdentityContext()
        : base("DefaultConnection")
        {
        }

        public virtual IDbSet<AspNetUser> Users { get; set; }
        public virtual IDbSet<User> UserDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserAspNetDbMap());
            modelBuilder.Configurations.Add(new UserDbMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
