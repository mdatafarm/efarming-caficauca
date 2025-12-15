namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLots : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.lots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            RenameColumn("dbo.invoices", "CardId", "Identification");
            AddColumn("dbo.invoices", "Yield", c => c.Double(nullable: false));
            AddColumn("dbo.invoices", "LotId", c => c.Guid());
            CreateIndex("dbo.invoices", "LotId");
            AddForeignKey("dbo.invoices", "LotId", "dbo.lots", "Id");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.invoices", "Identification", "CardId");
            DropForeignKey("dbo.invoices", "LotId", "dbo.lots");
            DropIndex("dbo.invoices", new[] { "LotId" });
            DropColumn("dbo.invoices", "LotId");
            DropColumn("dbo.invoices", "Yield");
            DropTable("dbo.lots");
        }
    }
}
