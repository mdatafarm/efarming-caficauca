namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCoffeTypeForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.invoices", "CoffeeTypeId");
            AddForeignKey("dbo.invoices", "CoffeeTypeId", "dbo.CoffeeTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.invoices", "CoffeeTypeId", "dbo.CoffeeTypes");
            DropIndex("dbo.invoices", new[] { "CoffeeTypeId" });
        }
    }
}
