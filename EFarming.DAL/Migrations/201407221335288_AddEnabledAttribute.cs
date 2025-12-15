namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnabledAttribute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Enabled", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Enabled");
        }
    }
}
