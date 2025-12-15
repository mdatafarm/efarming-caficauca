namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCoffeTypeInInvoices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.invoices", "CoffeeTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.invoices", "CoffeeTypeId");
        }
    }
}
