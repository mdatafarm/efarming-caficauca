namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoilTypesToFarm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FarmSoilTypes",
                c => new
                    {
                        FarmId = c.Guid(nullable: false),
                        SoilTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FarmId, t.SoilTypeId })
                .ForeignKey("dbo.farms", t => t.FarmId, cascadeDelete: true)
                .ForeignKey("dbo.SoilTypes", t => t.SoilTypeId, cascadeDelete: true)
                .Index(t => t.FarmId)
                .Index(t => t.SoilTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FarmSoilTypes", "SoilTypeId", "dbo.SoilTypes");
            DropForeignKey("dbo.FarmSoilTypes", "FarmId", "dbo.farms");
            DropIndex("dbo.FarmSoilTypes", new[] { "SoilTypeId" });
            DropIndex("dbo.FarmSoilTypes", new[] { "FarmId" });
            DropTable("dbo.FarmSoilTypes");
        }
    }
}
