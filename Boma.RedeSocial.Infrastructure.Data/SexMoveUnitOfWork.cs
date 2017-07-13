using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Profiles.Entities;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Profiles;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.DbMap.Users;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Boma.RedeSocial.Infrastructure.Data
{
    public class SexMoveUnitOfWork: IdentityDbContext, ISexMoveUnitOfWork
    {
        private ISexMoveContext SexMoveContext { get; }

        public SexMoveUnitOfWork()
            :base("DefaultConnection")
        {

        }

        protected SexMoveUnitOfWork(DbConnection connection, ISexMoveContext sexMoveContext): base(connection, false)
        {
            SexMoveContext = sexMoveContext;
        }

        public void Commit() => SaveChanges();

        private void CreateAudit(IEnumerable<DbEntityEntry> entries)
        {
            // Implementar 
        }

        #region Configurações do Contexto

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserDbMap());

            base.OnModelCreating(modelBuilder);
        }

        #endregion


        #region DbSets 
        
        public DbSet<User> Users { get; set; }

        //public DbSet<Profile> Profiles { get; set; }

        //public DbSet<ProfilePeopleConfiguration> ProfilePeopleConfigurations { get; set; }

        #endregion

        public static SexMoveUnitOfWork Create() => new SexMoveUnitOfWork();
    }
}
