namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlantationVarieties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlantationVarieties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        PlantationTypeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PlantationTypes", t => t.PlantationTypeId)
                .Index(t => t.PlantationTypeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PlantationVarieties", new[] { "PlantationTypeId" });
            DropForeignKey("dbo.PlantationVarieties", "PlantationTypeId", "dbo.PlantationTypes");
            DropTable("dbo.PlantationVarieties");
        }
    }
}
