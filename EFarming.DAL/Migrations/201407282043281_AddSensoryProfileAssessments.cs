namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSensoryProfileAssessments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SensoryProfileAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Answer = c.String(nullable: false),
                        SensoryProfileAssessmentId = c.Guid(nullable: false),
                        QualityAttributeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SensoryProfileAssessments", t => t.SensoryProfileAssessmentId)
                .ForeignKey("dbo.qualityAttributes", t => t.QualityAttributeId)
                .Index(t => t.SensoryProfileAssessmentId)
                .Index(t => t.QualityAttributeId);
            
            CreateTable(
                "dbo.SensoryProfileAssessments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 64),
                        FarmId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensoryProfileAnswers", "QualityAttributeId", "dbo.qualityAttributes");
            DropForeignKey("dbo.SensoryProfileAnswers", "SensoryProfileAssessmentId", "dbo.SensoryProfileAssessments");
            DropForeignKey("dbo.SensoryProfileAssessments", "FarmId", "dbo.farms");
            DropIndex("dbo.SensoryProfileAssessments", new[] { "FarmId" });
            DropIndex("dbo.SensoryProfileAnswers", new[] { "QualityAttributeId" });
            DropIndex("dbo.SensoryProfileAnswers", new[] { "SensoryProfileAssessmentId" });
            DropTable("dbo.SensoryProfileAssessments");
            DropTable("dbo.SensoryProfileAnswers");
        }
    }
}
