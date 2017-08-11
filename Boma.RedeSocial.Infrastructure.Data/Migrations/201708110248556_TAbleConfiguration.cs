namespace Boma.RedeSocial.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TAbleConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Key = c.String(),
                        Value = c.String(),
                        Profile_Id = c.Guid(),
                        Profile_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id)
                .ForeignKey("dbo.Profiles", t => t.Profile_Id1)
                .Index(t => t.Profile_Id)
                .Index(t => t.Profile_Id1);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ZipCode = c.String(),
                        Summary = c.String(),
                        Genre = c.Int(nullable: false),
                        MaritalStatus = c.Int(nullable: false),
                        MaritalStatusInterest = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        CreateBy = c.String(),
                        UpdatedAt = c.DateTime(nullable: false),
                        UpdateBy = c.String(),
                        DeletedAt = c.DateTime(),
                        DeleteBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Configurations", "Profile_Id1", "dbo.Profiles");
            DropForeignKey("dbo.Configurations", "Profile_Id", "dbo.Profiles");
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Configurations", new[] { "Profile_Id1" });
            DropIndex("dbo.Configurations", new[] { "Profile_Id" });
            DropTable("dbo.Profiles");
            DropTable("dbo.Configurations");
        }
    }
}
