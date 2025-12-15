namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssessmentTemplates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.assessmentTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.sensoryProfileAssessments", "AssessmentTemplateId", c => c.Guid(nullable: false));
            AddColumn("dbo.impactAssessments", "AssessmentTemplateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.impactAssessments", "AssessmentTemplateId");
            CreateIndex("dbo.sensoryProfileAssessments", "AssessmentTemplateId");
            AddForeignKey("dbo.impactAssessments", "AssessmentTemplateId", "dbo.assessmentTemplates", "Id");
            AddForeignKey("dbo.sensoryProfileAssessments", "AssessmentTemplateId", "dbo.assessmentTemplates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sensoryProfileAssessments", "AssessmentTemplateId", "dbo.assessmentTemplates");
            DropForeignKey("dbo.impactAssessments", "AssessmentTemplateId", "dbo.assessmentTemplates");
            DropIndex("dbo.sensoryProfileAssessments", new[] { "AssessmentTemplateId" });
            DropIndex("dbo.impactAssessments", new[] { "AssessmentTemplateId" });
            DropColumn("dbo.impactAssessments", "AssessmentTemplateId");
            DropColumn("dbo.sensoryProfileAssessments", "AssessmentTemplateId");
            DropTable("dbo.assessmentTemplates");
        }
    }
}
