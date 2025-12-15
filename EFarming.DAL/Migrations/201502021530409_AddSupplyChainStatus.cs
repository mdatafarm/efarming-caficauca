namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSupplyChainStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.supplyChainStatuses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.supplierChains", "SupplyChainStatusId", c => c.Guid());
            CreateIndex("dbo.supplierChains", "SupplyChainStatusId");
            AddForeignKey("dbo.supplierChains", "SupplyChainStatusId", "dbo.supplyChainStatuses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.supplierChains", "SupplyChainStatusId", "dbo.supplyChainStatuses");
            DropIndex("dbo.supplierChains", new[] { "SupplyChainStatusId" });
            DropColumn("dbo.supplierChains", "SupplyChainStatusId");
            DropTable("dbo.supplyChainStatuses");
        }
    }
}
