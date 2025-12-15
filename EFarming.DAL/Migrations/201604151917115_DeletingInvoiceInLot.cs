namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingInvoiceInLot : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.invoices", "Lot_Id", "dbo.lots");
            DropIndex("dbo.invoices", new[] { "Lot_Id" });
            DropColumn("dbo.invoices", "Lot_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.invoices", "Lot_Id", c => c.Guid());
            CreateIndex("dbo.invoices", "Lot_Id");
            AddForeignKey("dbo.invoices", "Lot_Id", "dbo.lots", "Id");
        }
    }
}
