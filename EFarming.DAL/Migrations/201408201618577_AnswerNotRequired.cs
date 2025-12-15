namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SensoryProfileAnswers", "Answer", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SensoryProfileAnswers", "Answer", c => c.String(nullable: false));
        }
    }
}
