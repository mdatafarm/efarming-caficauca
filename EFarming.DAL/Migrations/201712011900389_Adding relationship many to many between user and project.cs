namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingrelationshipmanytomanybetweenuserandproject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProjects",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ProjecId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProjecId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjecId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjecId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProjects", "ProjecId", "dbo.Projects");
            DropForeignKey("dbo.UserProjects", "UserId", "dbo.Users");
            DropIndex("dbo.UserProjects", new[] { "ProjecId" });
            DropIndex("dbo.UserProjects", new[] { "UserId" });
            DropTable("dbo.UserProjects");
        }
    }
}
