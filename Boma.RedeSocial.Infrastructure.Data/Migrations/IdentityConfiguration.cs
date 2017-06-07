namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class IdentityConfiguration : DbMigrationsConfiguration<Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager.SexMoveIdentityContext>
    {
        public IdentityConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager.SexMoveIdentityContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
