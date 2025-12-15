namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFertilizersToFarm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.fertilizers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        Measure = c.String(nullable: false, maxLength: 16),
                        FarmId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.fertilizers", "FarmId", "dbo.farms");
            DropIndex("dbo.fertilizers", new[] { "FarmId" });
            DropTable("dbo.fertilizers");
        }
    }
}
