namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingInvoiceReferenceForMoreInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialMoreInformation", "InvoiceReference", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialMoreInformation", "InvoiceReference");
        }
    }
}
