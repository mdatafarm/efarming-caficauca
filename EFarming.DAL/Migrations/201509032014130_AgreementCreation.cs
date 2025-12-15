namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgreementCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agreements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OurRef = c.String(),
                        Date = c.DateTime(nullable: false),
                        ClientId = c.Guid(nullable: false),
                        Volume = c.Int(nullable: false),
                        ShipmentDate = c.DateTime(nullable: false),
                        Quality = c.String(),
                        PriceLots = c.Int(nullable: false),
                        PriceDate = c.DateTime(nullable: false),
                        PriceUSD = c.Int(nullable: false),
                        Terms = c.String(),
                        Weights = c.String(),
                        Payment = c.String(),
                        Samples = c.String(),
                        Arbitration = c.String(),
                        Others = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agreements", "ClientId", "dbo.Clients");
            DropIndex("dbo.Agreements", new[] { "ClientId" });
            DropTable("dbo.Agreements");
        }
    }
}
