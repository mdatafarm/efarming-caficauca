namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductivityInfoToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.productivities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TotalHectares = c.Double(nullable: false),
                        InfraestructureHectares = c.Double(nullable: false),
                        ForestProtectedHectares = c.Double(nullable: false),
                        ConservationHectares = c.Double(nullable: false),
                        ShadingPercentage = c.Double(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.productivities", "Id", "dbo.farms");
            DropIndex("dbo.productivities", new[] { "Id" });
            DropTable("dbo.productivities");
        }
    }
}
