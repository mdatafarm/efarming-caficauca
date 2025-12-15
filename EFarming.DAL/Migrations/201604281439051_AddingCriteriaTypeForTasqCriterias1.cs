namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCriteriaTypeForTasqCriterias1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CriteriaTypes", newName: "TASQCriteriaTypes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TASQCriteriaTypes", newName: "CriteriaTypes");
        }
    }
}
