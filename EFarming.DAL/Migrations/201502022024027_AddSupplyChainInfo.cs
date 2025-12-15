namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplyChainInfo : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.supplierChains", newName: "supplyChains");
            RenameColumn(table: "dbo.farms", name: "SupplierChainId", newName: "SupplyChainId");
            RenameIndex(table: "dbo.farms", name: "IX_SupplierChainId", newName: "IX_SupplyChainId");
            AddColumn("dbo.supplyChains", "StartDate", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.supplyChains", "EndDate", c => c.DateTime(nullable: false, defaultValue: DateTime.UtcNow));
            AddColumn("dbo.supplyChains", "Potencial", c => c.Double(nullable: false, defaultValue: 0));
            AddColumn("dbo.supplyChains", "Bags", c => c.Double(nullable: false, defaultValue: 0));
            AddColumn("dbo.supplyChains", "DepartmentId", c => c.Guid());
            CreateIndex("dbo.supplyChains", "DepartmentId");
            AddForeignKey("dbo.supplyChains", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.supplyChains", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.supplyChains", new[] { "DepartmentId" });
            DropColumn("dbo.supplyChains", "DepartmentId");
            DropColumn("dbo.supplyChains", "Bags");
            DropColumn("dbo.supplyChains", "Potencial");
            DropColumn("dbo.supplyChains", "EndDate");
            DropColumn("dbo.supplyChains", "StartDate");
            RenameIndex(table: "dbo.farms", name: "IX_SupplyChainId", newName: "IX_SupplierChainId");
            RenameColumn(table: "dbo.farms", name: "SupplyChainId", newName: "SupplierChainId");
            RenameTable(name: "dbo.supplyChains", newName: "supplierChains");
        }
    }
}
