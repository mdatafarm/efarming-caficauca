namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FarmCooperativeIdNotRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.farms", new[] { "CooperativeId" });
            AlterColumn("dbo.farms", "CooperativeId", c => c.Guid());
            CreateIndex("dbo.farms", "CooperativeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.farms", new[] { "CooperativeId" });
            AlterColumn("dbo.farms", "CooperativeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.farms", "CooperativeId");
        }
    }
}
