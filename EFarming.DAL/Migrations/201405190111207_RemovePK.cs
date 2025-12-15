namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.farms", "FarmId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.farms", new[] { "Id" });
            AddPrimaryKey("dbo.farms", "FarmId");
            DropColumn("dbo.farms", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.farms", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.farms", new[] { "FarmId" });
            AddPrimaryKey("dbo.farms", "Id");
            DropColumn("dbo.farms", "FarmId");
        }
    }
}
