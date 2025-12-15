namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMandatoryFieldToCriteria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.criteria", "Mandatory", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.criteria", "Mandatory");
        }
    }
}
