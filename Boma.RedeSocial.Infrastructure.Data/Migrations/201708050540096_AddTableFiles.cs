namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableFiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ReferenceId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 8000, unicode: false),
                        ContentType = c.String(maxLength: 8000, unicode: false),
                        Size = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        DeletedAt = c.DateTime(),
                        DeleteBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Files");
        }
    }
}
