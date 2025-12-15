namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FarmSubstatusIsNotRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.farms", new[] { "FarmSubstatusId" });
            AlterColumn("dbo.farms", "FarmSubstatusId", c => c.Guid());
            CreateIndex("dbo.farms", "FarmSubstatusId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.farms", new[] { "FarmSubstatusId" });
            AlterColumn("dbo.farms", "FarmSubstatusId", c => c.Guid(nullable: false));
            CreateIndex("dbo.farms", "FarmSubstatusId");
        }
    }
}
