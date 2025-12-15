namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssessmentTemplatesToCategoriesAndQualityAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.categories", "AssessmentTemplateId", c => c.Guid(nullable: false));
            AddColumn("dbo.qualityAttributes", "AssessmentTemplateId", c => c.Guid(nullable: false));
            CreateIndex("dbo.categories", "AssessmentTemplateId");
            CreateIndex("dbo.qualityAttributes", "AssessmentTemplateId");
            AddForeignKey("dbo.categories", "AssessmentTemplateId", "dbo.assessmentTemplates", "Id");
            AddForeignKey("dbo.qualityAttributes", "AssessmentTemplateId", "dbo.assessmentTemplates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.qualityAttributes", "AssessmentTemplateId", "dbo.assessmentTemplates");
            DropForeignKey("dbo.categories", "AssessmentTemplateId", "dbo.assessmentTemplates");
            DropIndex("dbo.qualityAttributes", new[] { "AssessmentTemplateId" });
            DropIndex("dbo.categories", new[] { "AssessmentTemplateId" });
            DropColumn("dbo.qualityAttributes", "AssessmentTemplateId");
            DropColumn("dbo.categories", "AssessmentTemplateId");
        }
    }
}
