namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCooperativeFromInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.invoices", "CooperativeId", "dbo.cooperatives");
            DropIndex("dbo.invoices", new[] { "CooperativeId" });
            DropColumn("dbo.invoices", "CooperativeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.invoices", "CooperativeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.invoices", "CooperativeId");
            AddForeignKey("dbo.invoices", "CooperativeId", "dbo.cooperatives", "Id");
        }
    }
}
