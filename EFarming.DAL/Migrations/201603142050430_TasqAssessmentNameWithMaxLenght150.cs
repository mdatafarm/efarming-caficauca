namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasqAssessmentNameWithMaxLenght150 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TASQAssessments", "Description", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TASQAssessments", "Description", c => c.String());
        }
    }
}
