namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_FarmStatusId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.farms", "FarmStatus_Id", c => c.Guid());
            CreateIndex("dbo.farms", "FarmStatus_Id");
            AddForeignKey("dbo.farms", "FarmStatus_Id", "dbo.farmStatuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.farms", "FarmStatus_Id", "dbo.farmStatuses");
            DropIndex("dbo.farms", new[] { "FarmStatus_Id" });
            DropColumn("dbo.farms", "FarmStatus_Id");
        }
    }
}
