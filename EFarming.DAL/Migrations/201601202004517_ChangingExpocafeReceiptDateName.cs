namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingExpocafeReceiptDateName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialInvoices", "CafexportReceiptDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ComercialInvoices", "ExpocafeReceiptDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComercialInvoices", "ExpocafeReceiptDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ComercialInvoices", "CafexportReceiptDate");
        }
    }
}
