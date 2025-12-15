namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkersToFarm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.workers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PermanentWomen = c.Int(nullable: false),
                        PermanentMen = c.Int(nullable: false),
                        TemporaryWomen = c.Int(nullable: false),
                        TemporaryMen = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.workers", "Id", "dbo.farms");
            DropIndex("dbo.workers", new[] { "Id" });
            DropTable("dbo.workers");
        }
    }
}
