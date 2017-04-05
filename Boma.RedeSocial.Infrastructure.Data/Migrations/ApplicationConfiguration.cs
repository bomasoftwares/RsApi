namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ApplicationConfiguration : DbMigrationsConfiguration<Boma.RedeSocial.Infrastructure.Data.SexMoveUnitOfWork>
    {
        public ApplicationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Boma.RedeSocial.Infrastructure.Data.SexMoveUnitOfWork context)
        {
            
        }
    }
}
