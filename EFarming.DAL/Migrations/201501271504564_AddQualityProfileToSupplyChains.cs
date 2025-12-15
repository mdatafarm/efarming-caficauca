namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQualityProfileToSupplyChains : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.qualityProfiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.supplierChains", "QualityProfileId", c => c.Guid());
            CreateIndex("dbo.supplierChains", "QualityProfileId");
            AddForeignKey("dbo.supplierChains", "QualityProfileId", "dbo.qualityProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.supplierChains", "QualityProfileId", "dbo.qualityProfiles");
            DropIndex("dbo.supplierChains", new[] { "QualityProfileId" });
            DropColumn("dbo.supplierChains", "QualityProfileId");
            DropTable("dbo.qualityProfiles");
        }
    }
}
