namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlantationsToProductivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.plantations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Hectares = c.Double(nullable: false),
                        EstimatedProduction = c.Double(nullable: false),
                        Age = c.Int(nullable: false),
                        NumberOfPlants = c.Int(nullable: false),
                        PlantationStatusId = c.Guid(nullable: false),
                        ProductivityId = c.Guid(nullable: false),
                        PlantationTypeId = c.Guid(nullable: false),
                        PlantationVarietyId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlantationStatuses", t => t.PlantationStatusId)
                .ForeignKey("dbo.PlantationTypes", t => t.PlantationTypeId)
                .ForeignKey("dbo.PlantationVarieties", t => t.PlantationVarietyId)
                .ForeignKey("dbo.productivities", t => t.ProductivityId)
                .Index(t => t.PlantationStatusId)
                .Index(t => t.ProductivityId)
                .Index(t => t.PlantationTypeId)
                .Index(t => t.PlantationVarietyId);
            
            CreateTable(
                "dbo.floweringPeriods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        FloweringPeriodQualificationId = c.Guid(nullable: false),
                        PlantationId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloweringPeriodQualifications", t => t.FloweringPeriodQualificationId)
                .ForeignKey("dbo.plantations", t => t.PlantationId, cascadeDelete: true)
                .Index(t => t.FloweringPeriodQualificationId)
                .Index(t => t.PlantationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.plantations", "ProductivityId", "dbo.productivities");
            DropForeignKey("dbo.plantations", "PlantationVarietyId", "dbo.PlantationVarieties");
            DropForeignKey("dbo.plantations", "PlantationTypeId", "dbo.PlantationTypes");
            DropForeignKey("dbo.plantations", "PlantationStatusId", "dbo.PlantationStatuses");
            DropForeignKey("dbo.floweringPeriods", "PlantationId", "dbo.plantations");
            DropForeignKey("dbo.floweringPeriods", "FloweringPeriodQualificationId", "dbo.FloweringPeriodQualifications");
            DropIndex("dbo.floweringPeriods", new[] { "PlantationId" });
            DropIndex("dbo.floweringPeriods", new[] { "FloweringPeriodQualificationId" });
            DropIndex("dbo.plantations", new[] { "PlantationVarietyId" });
            DropIndex("dbo.plantations", new[] { "PlantationTypeId" });
            DropIndex("dbo.plantations", new[] { "ProductivityId" });
            DropIndex("dbo.plantations", new[] { "PlantationStatusId" });
            DropTable("dbo.floweringPeriods");
            DropTable("dbo.plantations");
        }
    }
}
