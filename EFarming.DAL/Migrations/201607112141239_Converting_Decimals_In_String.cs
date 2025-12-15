namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Converting_Decimals_In_String : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.plantations", "Hectares", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "TreesDistance", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "GrooveDistance", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "Density", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "EstimatedProduction", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.plantations", "EstimatedProduction", c => c.Double(nullable: false));
            AlterColumn("dbo.plantations", "Density", c => c.Double(nullable: false));
            AlterColumn("dbo.plantations", "GrooveDistance", c => c.Double(nullable: false));
            AlterColumn("dbo.plantations", "TreesDistance", c => c.Double(nullable: false));
            AlterColumn("dbo.plantations", "Hectares", c => c.Double(nullable: false));
        }
    }
}
