namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingOptionsForTasqCriteria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TASQCriterias", "Options", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TASQCriterias", "Options");
        }
    }
}
