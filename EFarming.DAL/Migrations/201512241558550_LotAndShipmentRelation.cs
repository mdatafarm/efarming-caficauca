namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LotAndShipmentRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComercialLots", "ContractInvoiceId", "dbo.ComercialInvoices");
            DropIndex("dbo.ComercialLots", new[] { "ContractInvoiceId" });
            AddColumn("dbo.ComercialLots", "ShipmentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ComercialLots", "ShipmentId");
            AddForeignKey("dbo.ComercialLots", "ShipmentId", "dbo.ComercialShipment", "Id");
            DropColumn("dbo.ComercialLots", "ContractInvoiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComercialLots", "ContractInvoiceId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.ComercialLots", "ShipmentId", "dbo.ComercialShipment");
            DropIndex("dbo.ComercialLots", new[] { "ShipmentId" });
            DropColumn("dbo.ComercialLots", "ShipmentId");
            CreateIndex("dbo.ComercialLots", "ContractInvoiceId");
            AddForeignKey("dbo.ComercialLots", "ContractInvoiceId", "dbo.ComercialInvoices", "Id");
        }
    }
}
