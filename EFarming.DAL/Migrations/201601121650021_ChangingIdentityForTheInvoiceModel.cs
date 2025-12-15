namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingIdentityForTheInvoiceModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComercialInvoices", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComercialInvoices", "Number", c => c.Int(nullable: false, identity: true));
        }
    }
}
