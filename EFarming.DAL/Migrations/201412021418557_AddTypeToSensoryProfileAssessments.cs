namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeToSensoryProfileAssessments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sensoryProfileAssessments", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.sensoryProfileAssessments", "Type");
        }
    }
}
