namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingTasqCriteria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TASQCriterias", "Short", c => c.String(maxLength: 50));
            AddColumn("dbo.TASQCriterias", "CriteriaOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TASQCriterias", "CriteriaOrder");
            DropColumn("dbo.TASQCriterias", "Short");
        }
    }
}
