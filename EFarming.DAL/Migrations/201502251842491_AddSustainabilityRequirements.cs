namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSustainabilityRequirements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requirements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.criteria", "RequirementId", c => c.Guid());
            CreateIndex("dbo.criteria", "RequirementId");
            AddForeignKey("dbo.criteria", "RequirementId", "dbo.Requirements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.criteria", "RequirementId", "dbo.Requirements");
            DropIndex("dbo.criteria", new[] { "RequirementId" });
            DropColumn("dbo.criteria", "RequirementId");
            DropTable("dbo.Requirements");
        }
    }
}
