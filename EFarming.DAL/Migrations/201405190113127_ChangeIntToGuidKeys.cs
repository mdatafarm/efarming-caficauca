namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIntToGuidKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.farms", "Id", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.farms", new[] { "FarmId" });
            AddPrimaryKey("dbo.farms", "Id");
            DropColumn("dbo.farms", "FarmId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.farms", "FarmId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.farms", new[] { "Id" });
            AddPrimaryKey("dbo.farms", "FarmId");
            DropColumn("dbo.farms", "Id");
        }
    }
}
