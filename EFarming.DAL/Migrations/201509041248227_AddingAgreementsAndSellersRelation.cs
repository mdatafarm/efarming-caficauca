namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAgreementsAndSellersRelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Agreements", new[] { "Seller_Id" });
            RenameColumn(table: "dbo.Agreements", name: "Seller_Id", newName: "SellerId");
            AlterColumn("dbo.Agreements", "SellerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Agreements", "SellerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Agreements", new[] { "SellerId" });
            AlterColumn("dbo.Agreements", "SellerId", c => c.Guid());
            RenameColumn(table: "dbo.Agreements", name: "SellerId", newName: "Seller_Id");
            CreateIndex("dbo.Agreements", "Seller_Id");
        }
    }
}
