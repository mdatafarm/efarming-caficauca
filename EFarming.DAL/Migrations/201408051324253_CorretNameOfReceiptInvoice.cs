namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorretNameOfReceiptInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.invoices", "Receipt", c => c.String(nullable: false, maxLength: 32));
            DropColumn("dbo.invoices", "Reciept");
        }
        
        public override void Down()
        {
            AddColumn("dbo.invoices", "Reciept", c => c.String(nullable: false, maxLength: 32));
            DropColumn("dbo.invoices", "Receipt");
        }
    }
}
