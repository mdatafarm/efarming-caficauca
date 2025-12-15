namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropingUnnecessaryInvoiceFields : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.invoices", name: "LotId", newName: "Lot_Id");
            RenameIndex(table: "dbo.invoices", name: "IX_LotId", newName: "IX_Lot_Id");
            DropColumn("dbo.invoices", "Yield");
        }
        
        public override void Down()
        {
            AddColumn("dbo.invoices", "Yield", c => c.Double(nullable: false));
            RenameIndex(table: "dbo.invoices", name: "IX_Lot_Id", newName: "IX_LotId");
            RenameColumn(table: "dbo.invoices", name: "Lot_Id", newName: "LotId");
        }
    }
}
