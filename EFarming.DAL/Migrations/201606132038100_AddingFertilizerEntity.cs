namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFertilizerEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.fertilizers", "InvoiceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "FarmerIdentification", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "Ubication", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "Value", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "Hold", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "CashRegister", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "UnitPrice", c => c.Int(nullable: false));
            AddColumn("dbo.fertilizers", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.fertilizers", "Measure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.fertilizers", "Measure", c => c.Double(nullable: false));
            DropColumn("dbo.fertilizers", "Quantity");
            DropColumn("dbo.fertilizers", "UnitPrice");
            DropColumn("dbo.fertilizers", "CashRegister");
            DropColumn("dbo.fertilizers", "Hold");
            DropColumn("dbo.fertilizers", "Value");
            DropColumn("dbo.fertilizers", "Ubication");
            DropColumn("dbo.fertilizers", "FarmerIdentification");
            DropColumn("dbo.fertilizers", "InvoiceNumber");
        }
    }
}
