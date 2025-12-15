namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherActivitiesToFarm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.farmOtherActivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Percentage = c.Double(nullable: false),
                        FarmId = c.Guid(nullable: false),
                        OtherActivityId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .ForeignKey("dbo.OtherActivities", t => t.OtherActivityId)
                .Index(t => t.FarmId)
                .Index(t => t.OtherActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.farmOtherActivities", "OtherActivityId", "dbo.OtherActivities");
            DropForeignKey("dbo.farmOtherActivities", "FarmId", "dbo.farms");
            DropIndex("dbo.farmOtherActivities", new[] { "OtherActivityId" });
            DropIndex("dbo.farmOtherActivities", new[] { "FarmId" });
            DropTable("dbo.farmOtherActivities");
        }
    }
}
