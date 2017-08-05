namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableFiles1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "Name", c => c.String(maxLength: 200, unicode: false));
            AlterColumn("dbo.Files", "ContentType", c => c.String(maxLength: 200, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "ContentType", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Files", "Name", c => c.String(maxLength: 8000, unicode: false));
        }
    }
}
