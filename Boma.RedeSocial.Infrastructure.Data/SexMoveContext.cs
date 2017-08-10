﻿using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Files.Entities;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using Boma.RedeSocial.Domain.Profiles.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Profiles;

namespace Boma.RedeSocial.Infrastructure.Data
{
    public class SexMoveContext: IdentityDbContext
    {
        public SexMoveContext()
            :base("DefaultConnection")
        {

        }

        public void Commit() => SaveChanges();


        #region Configurações do Contexto

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserDbMap());
            modelBuilder.Configurations.Add(new FileDbMap());
            modelBuilder.Configurations.Add(new ProfileDbMap());

            base.OnModelCreating(modelBuilder);
        }

        #endregion


        #region DbSets 
        
        public DbSet<User> AppUsers { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Profile> UserProfile { get; set; }
        #endregion


    }
}
