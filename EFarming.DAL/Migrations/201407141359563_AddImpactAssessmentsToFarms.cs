namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImpactAssessmentsToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.impactAssessments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false, maxLength: 128),
                        FarmId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId);
            
            CreateTable(
                "dbo.ImpactAssessmentAnswers",
                c => new
                    {
                        ImpactAssessmentId = c.Guid(nullable: false),
                        CriteriaOptionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ImpactAssessmentId, t.CriteriaOptionId })
                .ForeignKey("dbo.impactAssessments", t => t.ImpactAssessmentId, cascadeDelete: true)
                .ForeignKey("dbo.criteriaOptions", t => t.CriteriaOptionId, cascadeDelete: true)
                .Index(t => t.ImpactAssessmentId)
                .Index(t => t.CriteriaOptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.impactAssessments", "FarmId", "dbo.farms");
            DropForeignKey("dbo.ImpactAssessmentAnswers", "CriteriaOptionId", "dbo.criteriaOptions");
            DropForeignKey("dbo.ImpactAssessmentAnswers", "ImpactAssessmentId", "dbo.impactAssessments");
            DropIndex("dbo.ImpactAssessmentAnswers", new[] { "CriteriaOptionId" });
            DropIndex("dbo.ImpactAssessmentAnswers", new[] { "ImpactAssessmentId" });
            DropIndex("dbo.impactAssessments", new[] { "FarmId" });
            DropTable("dbo.ImpactAssessmentAnswers");
            DropTable("dbo.impactAssessments");
        }
    }
}
