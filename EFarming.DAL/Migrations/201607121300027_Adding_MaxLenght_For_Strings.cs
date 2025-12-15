namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_MaxLenght_For_Strings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.plantations", "Hectares", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.plantations", "TreesDistance", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.plantations", "GrooveDistance", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.plantations", "Density", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.plantations", "EstimatedProduction", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.plantations", "EstimatedProduction", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "Density", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "GrooveDistance", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "TreesDistance", c => c.String(nullable: false));
            AlterColumn("dbo.plantations", "Hectares", c => c.String(nullable: false));
        }
    }
}
