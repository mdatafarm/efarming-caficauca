namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingAssessmentIdField : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TASQAssessmentAnswers", new[] { "TASQAssessment_Id" });
            RenameColumn(table: "dbo.TASQAssessmentAnswers", name: "TASQAssessment_Id", newName: "TASQAssessmentId");
            AlterColumn("dbo.TASQAssessmentAnswers", "TASQAssessmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TASQAssessmentAnswers", "TASQAssessmentId");
            DropColumn("dbo.TASQAssessmentAnswers", "AssessmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TASQAssessmentAnswers", "AssessmentId", c => c.Guid(nullable: false));
            DropIndex("dbo.TASQAssessmentAnswers", new[] { "TASQAssessmentId" });
            AlterColumn("dbo.TASQAssessmentAnswers", "TASQAssessmentId", c => c.Guid());
            RenameColumn(table: "dbo.TASQAssessmentAnswers", name: "TASQAssessmentId", newName: "TASQAssessment_Id");
            CreateIndex("dbo.TASQAssessmentAnswers", "TASQAssessment_Id");
        }
    }
}
