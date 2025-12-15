namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryAndSupplierToFarm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.supplierChains",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        SupplierId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.suppliers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        CountryId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.farms", "SupplierChainId", c => c.Guid());
            CreateIndex("dbo.farms", "SupplierChainId");
            AddForeignKey("dbo.farms", "SupplierChainId", "dbo.supplierChains", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.supplierChains", "SupplierId", "dbo.suppliers");
            DropForeignKey("dbo.suppliers", "CountryId", "dbo.countries");
            DropForeignKey("dbo.farms", "SupplierChainId", "dbo.supplierChains");
            DropIndex("dbo.suppliers", new[] { "CountryId" });
            DropIndex("dbo.supplierChains", new[] { "SupplierId" });
            DropIndex("dbo.farms", new[] { "SupplierChainId" });
            DropColumn("dbo.farms", "SupplierChainId");
            DropTable("dbo.countries");
            DropTable("dbo.suppliers");
            DropTable("dbo.supplierChains");
        }
    }
}
