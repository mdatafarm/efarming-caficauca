namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingTasqAssessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TASQAssessments", "SyncOperation", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TASQAssessments", "SyncOperation");
        }
    }
}
