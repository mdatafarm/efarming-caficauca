namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssociatedPeopleToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.farmAssociatedPeople",
                c => new
                    {
                        FarmId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.FarmId, t.UserId })
                .ForeignKey("dbo.farms", t => t.FarmId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FarmId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.farmAssociatedPeople", "UserId", "dbo.Users");
            DropForeignKey("dbo.farmAssociatedPeople", "FarmId", "dbo.farms");
            DropIndex("dbo.farmAssociatedPeople", new[] { "UserId" });
            DropIndex("dbo.farmAssociatedPeople", new[] { "FarmId" });
            DropTable("dbo.farmAssociatedPeople");
        }
    }
}
