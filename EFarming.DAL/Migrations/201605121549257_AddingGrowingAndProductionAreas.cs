namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGrowingAndProductionAreas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "productionArea", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "growingArea", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productivities", "growingArea");
            DropColumn("dbo.productivities", "productionArea");
        }
    }
}
