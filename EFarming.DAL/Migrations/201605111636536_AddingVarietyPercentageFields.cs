namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingVarietyPercentageFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "averageDensity", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "percentageColombia", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "percentageCaturra", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "percentageCastillo", c => c.Double(nullable: false));
            AddColumn("dbo.productivities", "percentageotra", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productivities", "percentageotra");
            DropColumn("dbo.productivities", "percentageCastillo");
            DropColumn("dbo.productivities", "percentageCaturra");
            DropColumn("dbo.productivities", "percentageColombia");
            DropColumn("dbo.productivities", "averageDensity");
        }
    }
}
