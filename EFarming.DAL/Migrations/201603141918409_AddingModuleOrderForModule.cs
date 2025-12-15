namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingModuleOrderForModule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TASQModules", "ModuleOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TASQModules", "ModuleOrder");
        }
    }
}
