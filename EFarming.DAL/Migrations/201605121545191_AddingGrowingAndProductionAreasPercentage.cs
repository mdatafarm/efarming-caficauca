namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGrowingAndProductionAreasPercentage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "productionAreaPercentage", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "growingAreaPercentage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productivities", "growingAreaPercentage");
            DropColumn("dbo.productivities", "productionAreaPercentage");
        }
    }
}
