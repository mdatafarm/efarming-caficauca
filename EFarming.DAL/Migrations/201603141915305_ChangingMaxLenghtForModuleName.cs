namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingMaxLenghtForModuleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TASQModules", "Name", c => c.String(maxLength: 150));
            DropColumn("dbo.TASQModules", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TASQModules", "Description", c => c.String());
            DropColumn("dbo.TASQModules", "Name");
        }
    }
}
