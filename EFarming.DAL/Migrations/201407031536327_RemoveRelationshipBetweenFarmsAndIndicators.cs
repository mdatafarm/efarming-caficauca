namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelationshipBetweenFarmsAndIndicators : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FarmIndicators", "IndicatorId", "dbo.indicators");
            DropForeignKey("dbo.FarmIndicators", "FarmId", "dbo.farms");
            DropIndex("dbo.FarmIndicators", new[] { "IndicatorId" });
            DropIndex("dbo.FarmIndicators", new[] { "FarmId" });
            DropTable("dbo.FarmIndicators");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FarmIndicators",
                c => new
                    {
                        IndicatorId = c.Guid(nullable: false),
                        FarmId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndicatorId, t.FarmId });
            
            CreateIndex("dbo.FarmIndicators", "FarmId");
            CreateIndex("dbo.FarmIndicators", "IndicatorId");
            AddForeignKey("dbo.FarmIndicators", "FarmId", "dbo.farms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FarmIndicators", "IndicatorId", "dbo.indicators", "Id", cascadeDelete: true);
        }
    }
}
