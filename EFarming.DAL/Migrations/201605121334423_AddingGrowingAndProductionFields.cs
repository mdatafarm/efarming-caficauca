namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGrowingAndProductionFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "productionPlants", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "productionPercentage", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "growingPlants", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "growingPercentage", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "estimatedProduction", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productivities", "estimatedProduction");
            DropColumn("dbo.productivities", "growingPercentage");
            DropColumn("dbo.productivities", "growingPlants");
            DropColumn("dbo.productivities", "productionPercentage");
            DropColumn("dbo.productivities", "productionPlants");
        }
    }
}
