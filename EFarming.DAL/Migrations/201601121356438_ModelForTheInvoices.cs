namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelForTheInvoices : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ComercialInvoices", "AgreementId", "dbo.ComercialAgreements");
            DropIndex("dbo.ComercialInvoices", new[] { "AgreementId" });
            AddColumn("dbo.ComercialInvoices", "ExpocafeReceiptDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ComercialInvoices", "CafexportPaymentDeadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ComercialInvoices", "ClientPaymentDeadLine", c => c.DateTime(nullable: false));
            AddColumn("dbo.ComercialInvoices", "CafexportPaymentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ComercialInvoices", "ClientPaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ComercialInvoices", "Number", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.ComercialInvoices", "Id");
            AddForeignKey("dbo.ComercialInvoices", "Id", "dbo.ComercialShipment", "Id");
            DropColumn("dbo.ComercialInvoices", "Date");
            DropColumn("dbo.ComercialInvoices", "AgreementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComercialInvoices", "AgreementId", c => c.Guid(nullable: false));
            AddColumn("dbo.ComercialInvoices", "Date", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.ComercialInvoices", "Id", "dbo.ComercialShipment");
            DropIndex("dbo.ComercialInvoices", new[] { "Id" });
            AlterColumn("dbo.ComercialInvoices", "Number", c => c.Int(nullable: false));
            DropColumn("dbo.ComercialInvoices", "ClientPaymentDate");
            DropColumn("dbo.ComercialInvoices", "CafexportPaymentDate");
            DropColumn("dbo.ComercialInvoices", "ClientPaymentDeadLine");
            DropColumn("dbo.ComercialInvoices", "CafexportPaymentDeadline");
            DropColumn("dbo.ComercialInvoices", "ExpocafeReceiptDate");
            CreateIndex("dbo.ComercialInvoices", "AgreementId");
            AddForeignKey("dbo.ComercialInvoices", "AgreementId", "dbo.ComercialAgreements", "Id");
        }
    }
}
