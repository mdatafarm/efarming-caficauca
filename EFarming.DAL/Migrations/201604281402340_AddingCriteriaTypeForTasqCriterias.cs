namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCriteriaTypeForTasqCriterias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CriteriaTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionType = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TASQCriterias", "CriteriaTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.TASQCriterias", "CriteriaTypeId");
            AddForeignKey("dbo.TASQCriterias", "CriteriaTypeId", "dbo.CriteriaTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TASQCriterias", "CriteriaTypeId", "dbo.CriteriaTypes");
            DropIndex("dbo.TASQCriterias", new[] { "CriteriaTypeId" });
            DropColumn("dbo.TASQCriterias", "CriteriaTypeId");
            DropTable("dbo.CriteriaTypes");
        }
    }
}
