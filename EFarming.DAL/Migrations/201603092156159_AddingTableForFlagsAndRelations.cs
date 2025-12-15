namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTableForFlagsAndRelations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TASQFlagIndicators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TASQAssessments", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.TASQCriterias", "FlagIndicatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.TASQAssessments", "UserId");
            CreateIndex("dbo.TASQCriterias", "FlagIndicatorId");
            AddForeignKey("dbo.TASQCriterias", "FlagIndicatorId", "dbo.TASQFlagIndicators", "Id");
            AddForeignKey("dbo.TASQAssessments", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TASQAssessments", "UserId", "dbo.Users");
            DropForeignKey("dbo.TASQCriterias", "FlagIndicatorId", "dbo.TASQFlagIndicators");
            DropIndex("dbo.TASQCriterias", new[] { "FlagIndicatorId" });
            DropIndex("dbo.TASQAssessments", new[] { "UserId" });
            DropColumn("dbo.TASQCriterias", "FlagIndicatorId");
            DropColumn("dbo.TASQAssessments", "UserId");
            DropTable("dbo.TASQFlagIndicators");
        }
    }
}
