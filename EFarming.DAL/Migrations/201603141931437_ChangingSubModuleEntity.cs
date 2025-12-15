namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingSubModuleEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TASQSubModules", "Name", c => c.String(maxLength: 150));
            AddColumn("dbo.TASQSubModules", "SubModuleOrder", c => c.Int(nullable: false));
            DropColumn("dbo.TASQSubModules", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TASQSubModules", "Description", c => c.String());
            DropColumn("dbo.TASQSubModules", "SubModuleOrder");
            DropColumn("dbo.TASQSubModules", "Name");
        }
    }
}
