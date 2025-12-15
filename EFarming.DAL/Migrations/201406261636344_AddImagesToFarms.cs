namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Size = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        Thumb = c.String(nullable: false),
                        FarmId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "FarmId", "dbo.farms");
            DropIndex("dbo.Images", new[] { "FarmId" });
            DropTable("dbo.Images");
        }
    }
}
