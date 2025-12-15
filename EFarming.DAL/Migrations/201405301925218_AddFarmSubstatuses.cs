namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFarmSubstatuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FarmSubstatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        FarmStatusId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farmStatuses", t => t.FarmStatusId)
                .Index(t => t.FarmStatusId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.FarmSubstatus", new[] { "FarmStatusId" });
            DropForeignKey("dbo.FarmSubstatus", "FarmStatusId", "dbo.farmStatuses");
            DropTable("dbo.FarmSubstatus");
        }
    }
}
