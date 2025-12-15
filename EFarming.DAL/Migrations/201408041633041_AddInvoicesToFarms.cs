namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoicesToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.invoices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Reciept = c.String(nullable: false, maxLength: 32),
                        CardId = c.String(nullable: false, maxLength: 32),
                        Weight = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        FarmId = c.Guid(nullable: false),
                        CooperativeId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.cooperatives", t => t.CooperativeId)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId)
                .Index(t => t.CooperativeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.invoices", "FarmId", "dbo.farms");
            DropForeignKey("dbo.invoices", "CooperativeId", "dbo.cooperatives");
            DropIndex("dbo.invoices", new[] { "CooperativeId" });
            DropIndex("dbo.invoices", new[] { "FarmId" });
            DropTable("dbo.invoices");
        }
    }
}
