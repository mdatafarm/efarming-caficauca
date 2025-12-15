namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAssessmentFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.assessmentTemplates", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.assessmentTemplates", "ExpiredOn", c => c.DateTime());
            AddColumn("dbo.assessmentTemplates", "CreatedBy", c => c.Guid());
            AddColumn("dbo.assessmentTemplates", "Published", c => c.Boolean());
            AddColumn("dbo.assessmentTemplates", "Public", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.assessmentTemplates", "Public");
            DropColumn("dbo.assessmentTemplates", "Published");
            DropColumn("dbo.assessmentTemplates", "CreatedBy");
            DropColumn("dbo.assessmentTemplates", "ExpiredOn");
            DropColumn("dbo.assessmentTemplates", "CreatedOn");
        }
    }
}
