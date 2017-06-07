﻿using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Profiles;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager
{
    public class SexMoveIdentityContext : IdentityDbContext
    {
        public SexMoveIdentityContext()
        : base("DefaultConnection")
        {
        }

        public virtual IDbSet<AspNetUser> Users { get; set; }
        public virtual IDbSet<User> UserDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SexMoveIdentityContext>(null);
            modelBuilder.Configurations.Add(new UserAspNetDbMap());
            
            modelBuilder.Configurations.Add(new ProfileDbMap());
            modelBuilder.Configurations.Add(new ProfilePeopleConfigurationDbMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
