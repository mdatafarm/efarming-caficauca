namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingInvoicesEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.invoices", "InvoiceNumber", c => c.Int(nullable: false));
            AddColumn("dbo.invoices", "Value", c => c.Double(nullable: false));
            AddColumn("dbo.invoices", "DateInvoice", c => c.DateTime(nullable: false));
            AddColumn("dbo.invoices", "Ubication", c => c.Int(nullable: false));
            AddColumn("dbo.invoices", "Hold", c => c.Int(nullable: false));
            AddColumn("dbo.invoices", "Cash", c => c.Int(nullable: false));
            AddColumn("dbo.invoices", "BaseKg", c => c.Int(nullable: false));
            DropColumn("dbo.invoices", "Receipt");
            DropColumn("dbo.invoices", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.invoices", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.invoices", "Receipt", c => c.String(nullable: false, maxLength: 32));
            DropColumn("dbo.invoices", "BaseKg");
            DropColumn("dbo.invoices", "Cash");
            DropColumn("dbo.invoices", "Hold");
            DropColumn("dbo.invoices", "Ubication");
            DropColumn("dbo.invoices", "DateInvoice");
            DropColumn("dbo.invoices", "Value");
            DropColumn("dbo.invoices", "InvoiceNumber");
        }
    }
}
