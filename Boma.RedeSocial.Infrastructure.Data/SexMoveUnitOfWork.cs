using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Interfaces;
using Boma.RedeSocial.Domain.Interfaces.Entities;
using Boma.RedeSocial.Domain.Users;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.DbModel;
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
            throw new NotImplementedException();
        }

        public void Commit() => SaveChanges();

        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<AspNetUserDbModel> AspNetUsers { get; set; }


        #endregion

    }
}
