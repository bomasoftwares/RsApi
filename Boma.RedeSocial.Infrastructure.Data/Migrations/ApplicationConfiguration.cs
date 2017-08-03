namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class ApplicationConfiguration : DbMigrationsConfiguration<SexMoveContext>
    {
        public ApplicationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SexMoveContext context)
        {
            
        }
    }
}
