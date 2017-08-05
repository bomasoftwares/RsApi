using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Files.Entities;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

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

            base.OnModelCreating(modelBuilder);
        }

        #endregion


        #region DbSets 
        
        public DbSet<User> AppUsers { get; set; }
        public DbSet<File> Files { get; set; }

        #endregion

        
    }
}
