namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingPlantationFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.plantations", "TreesDistance", c => c.Double(nullable: false));
            AddColumn("dbo.plantations", "GrooveDistance", c => c.Double(nullable: false));
            AddColumn("dbo.plantations", "Density", c => c.Double(nullable: false));
            AlterColumn("dbo.plantations", "Age", c => c.DateTime(nullable: false));
            DropColumn("dbo.plantations", "NumberOfProductivePlants");
        }
        
        public override void Down()
        {
            AddColumn("dbo.plantations", "NumberOfProductivePlants", c => c.Int(nullable: false));
            AlterColumn("dbo.plantations", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.plantations", "Density");
            DropColumn("dbo.plantations", "GrooveDistance");
            DropColumn("dbo.plantations", "TreesDistance");
        }
    }
}
