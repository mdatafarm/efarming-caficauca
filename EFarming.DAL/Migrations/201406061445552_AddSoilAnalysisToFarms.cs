namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoilAnalysisToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.soilAnalysis",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(nullable: false),
                        Depth = c.Int(nullable: false),
                        FarmId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.soilAnalysis", "FarmId", "dbo.farms");
            DropIndex("dbo.soilAnalysis", new[] { "FarmId" });
            DropTable("dbo.soilAnalysis");
        }
    }
}
