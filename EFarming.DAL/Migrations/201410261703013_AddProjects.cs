namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FarmProjects",
                c => new
                    {
                        FarmId = c.Guid(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FarmId, t.ProjectId })
                .ForeignKey("dbo.farms", t => t.FarmId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.FarmId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FarmProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.FarmProjects", "FarmId", "dbo.farms");
            DropIndex("dbo.FarmProjects", new[] { "ProjectId" });
            DropIndex("dbo.FarmProjects", new[] { "FarmId" });
            DropTable("dbo.FarmProjects");
            DropTable("dbo.Projects");
        }
    }
}
