namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesForInvoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComercialInvoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AgreementId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComercialAgreements", t => t.AgreementId)
                .Index(t => t.AgreementId);
            
            CreateTable(
                "dbo.ComercialLots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LotReference = c.String(nullable: false),
                        ContractInvoiceId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComercialInvoices", t => t.ContractInvoiceId)
                .Index(t => t.ContractInvoiceId);
            
            CreateTable(
                "dbo.ComercialReferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Type = c.String(),
                        ClientId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComercialClients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ComercialReferencesRelationShip",
                c => new
                    {
                        DocumentId = c.Guid(nullable: false),
                        DocumentReferenceId = c.Int(nullable: false),
                        Value = c.String(),
                        Id = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.DocumentId, t.DocumentReferenceId })
                .ForeignKey("dbo.ComercialReferences", t => t.DocumentReferenceId)
                .Index(t => t.DocumentReferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComercialReferencesRelationShip", "DocumentReferenceId", "dbo.ComercialReferences");
            DropForeignKey("dbo.ComercialReferences", "ClientId", "dbo.ComercialClients");
            DropForeignKey("dbo.ComercialLots", "ContractInvoiceId", "dbo.ComercialInvoices");
            DropForeignKey("dbo.ComercialInvoices", "AgreementId", "dbo.ComercialAgreements");
            DropIndex("dbo.ComercialReferencesRelationShip", new[] { "DocumentReferenceId" });
            DropIndex("dbo.ComercialReferences", new[] { "ClientId" });
            DropIndex("dbo.ComercialLots", new[] { "ContractInvoiceId" });
            DropIndex("dbo.ComercialInvoices", new[] { "AgreementId" });
            DropTable("dbo.ComercialReferencesRelationShip");
            DropTable("dbo.ComercialReferences");
            DropTable("dbo.ComercialLots");
            DropTable("dbo.ComercialInvoices");
        }
    }
}
