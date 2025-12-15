namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Converting_Double_To_String_Productivity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.productivities", "TotalHectares", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.productivities", "InfrastructureHectares", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.productivities", "ForestProtectedHectares", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.productivities", "ConservationHectares", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.productivities", "ShadingPercentage", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.productivities", "averageDensity", c => c.String(maxLength: 20));
            AlterColumn("dbo.productivities", "coffeeArea", c => c.String(maxLength: 20));
            AlterColumn("dbo.productivities", "productionArea", c => c.String(maxLength: 20));
            AlterColumn("dbo.productivities", "growingArea", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.productivities", "growingArea", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "productionArea", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "coffeeArea", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "averageDensity", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "ShadingPercentage", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "ConservationHectares", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "ForestProtectedHectares", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "InfrastructureHectares", c => c.Double(nullable: false));
            AlterColumn("dbo.productivities", "TotalHectares", c => c.Double(nullable: false));
        }
    }
}
