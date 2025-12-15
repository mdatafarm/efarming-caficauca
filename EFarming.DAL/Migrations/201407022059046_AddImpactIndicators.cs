namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImpactIndicators : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.criteria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 32),
                        Value = c.Int(nullable: false),
                        IndicatorId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.indicators", t => t.IndicatorId)
                .Index(t => t.IndicatorId);
            
            CreateTable(
                "dbo.criteriaOptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 32),
                        Value = c.Int(nullable: false),
                        CriteriaId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.criteria", t => t.CriteriaId)
                .Index(t => t.CriteriaId);
            
            CreateTable(
                "dbo.indicators",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 16),
                        Description = c.String(maxLength: 32),
                        Scale = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FarmIndicators",
                c => new
                    {
                        IndicatorId = c.Guid(nullable: false),
                        FarmId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndicatorId, t.FarmId })
                .ForeignKey("dbo.indicators", t => t.IndicatorId, cascadeDelete: true)
                .ForeignKey("dbo.farms", t => t.FarmId, cascadeDelete: true)
                .Index(t => t.IndicatorId)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FarmIndicators", "FarmId", "dbo.farms");
            DropForeignKey("dbo.FarmIndicators", "IndicatorId", "dbo.indicators");
            DropForeignKey("dbo.criteria", "IndicatorId", "dbo.indicators");
            DropForeignKey("dbo.criteriaOptions", "CriteriaId", "dbo.criteria");
            DropIndex("dbo.FarmIndicators", new[] { "FarmId" });
            DropIndex("dbo.FarmIndicators", new[] { "IndicatorId" });
            DropIndex("dbo.criteriaOptions", new[] { "CriteriaId" });
            DropIndex("dbo.criteria", new[] { "IndicatorId" });
            DropTable("dbo.FarmIndicators");
            DropTable("dbo.indicators");
            DropTable("dbo.criteriaOptions");
            DropTable("dbo.criteria");
        }
    }
}
