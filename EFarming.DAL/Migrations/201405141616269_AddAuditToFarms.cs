namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuditToFarms : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.farms", "CreatedAt", c => c.DateTime(nullable: true));
            AddColumn("dbo.farms", "UpdatedAt", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.farms", "UpdatedAt");
            DropColumn("dbo.farms", "CreatedAt");
        }
    }
}
