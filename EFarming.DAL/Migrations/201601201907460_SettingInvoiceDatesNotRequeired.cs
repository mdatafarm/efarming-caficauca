namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SettingInvoiceDatesNotRequeired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComercialInvoices", "CafexportPaymentDeadline", c => c.DateTime());
            AlterColumn("dbo.ComercialInvoices", "ClientPaymentDeadLine", c => c.DateTime());
            AlterColumn("dbo.ComercialInvoices", "CafexportPaymentDate", c => c.DateTime());
            AlterColumn("dbo.ComercialInvoices", "ClientPaymentDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComercialInvoices", "ClientPaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ComercialInvoices", "CafexportPaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ComercialInvoices", "ClientPaymentDeadLine", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ComercialInvoices", "CafexportPaymentDeadline", c => c.DateTime(nullable: false));
        }
    }
}
