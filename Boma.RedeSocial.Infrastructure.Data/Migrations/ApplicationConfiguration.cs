namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class ApplicationConfiguration : DbMigrationsConfiguration<SexMoveContext>
    {
        public ApplicationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SexMoveContext context)
        {
            
        }
    }
}
