namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SellerCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Footer = c.String(),
                        Header = c.String(),
                        SubHeader = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Agreements", "Seller_Id", c => c.Guid());
            CreateIndex("dbo.Agreements", "Seller_Id");
            AddForeignKey("dbo.Agreements", "Seller_Id", "dbo.Sellers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agreements", "Seller_Id", "dbo.Sellers");
            DropIndex("dbo.Agreements", new[] { "Seller_Id" });
            DropColumn("dbo.Agreements", "Seller_Id");
            DropTable("dbo.Sellers");
        }
    }
}
