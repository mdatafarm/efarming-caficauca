namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableForShipment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComercialShipment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DocumentBL = c.String(nullable: false),
                        ShippingDate = c.DateTime(nullable: false),
                        PortOfLanding = c.String(),
                        PortOfDestination = c.String(),
                        Vessel = c.String(),
                        ShippingLine = c.String(),
                        ExpocafeInvoice = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ComercialShipment");
        }
    }
}
