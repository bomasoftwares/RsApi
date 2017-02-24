using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Users.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Boma.RedeSocial.Infrastructure.Data
{
    public class SexMoveUnitOfWork: DbContext, ISexMoveUnitOfWork
    {
        public SexMoveUnitOfWork(DbConnection connection, ISexMoveContext sexMoveContext): base(connection, false)
        {
            SexMoveContext = sexMoveContext;
        }

        private ISexMoveContext SexMoveContext { get; }

        public override int SaveChanges()
        {
            var entriesChangeds = ChangeTracker.Entries();
            CreateAudit(entriesChangeds);
            return base.SaveChanges();
        }

        private void CreateAudit(IEnumerable<DbEntityEntry> entries)
        {
            // Implementar 
        }

        public void Commit() => SaveChanges();

        #region DbSets 

        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

    }
}
