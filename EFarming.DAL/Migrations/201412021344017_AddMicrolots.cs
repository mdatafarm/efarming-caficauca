namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMicrolots : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.sensoryProfileAssessments", new[] { "FarmId" });
            CreateTable(
                "dbo.Microlots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.sensoryProfileAssessments", "InvoiceId", c => c.Guid());
            AddColumn("dbo.sensoryProfileAssessments", "MicrolotId", c => c.Guid());
            AlterColumn("dbo.sensoryProfileAssessments", "FarmId", c => c.Guid());
            CreateIndex("dbo.sensoryProfileAssessments", "FarmId");
            CreateIndex("dbo.sensoryProfileAssessments", "InvoiceId");
            CreateIndex("dbo.sensoryProfileAssessments", "MicrolotId");
            AddForeignKey("dbo.sensoryProfileAssessments", "MicrolotId", "dbo.Microlots", "Id");
            AddForeignKey("dbo.sensoryProfileAssessments", "InvoiceId", "dbo.invoices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sensoryProfileAssessments", "InvoiceId", "dbo.invoices");
            DropForeignKey("dbo.sensoryProfileAssessments", "MicrolotId", "dbo.Microlots");
            DropIndex("dbo.sensoryProfileAssessments", new[] { "MicrolotId" });
            DropIndex("dbo.sensoryProfileAssessments", new[] { "InvoiceId" });
            DropIndex("dbo.sensoryProfileAssessments", new[] { "FarmId" });
            AlterColumn("dbo.sensoryProfileAssessments", "FarmId", c => c.Guid(nullable: false));
            DropColumn("dbo.sensoryProfileAssessments", "MicrolotId");
            DropColumn("dbo.sensoryProfileAssessments", "InvoiceId");
            DropTable("dbo.Microlots");
            CreateIndex("dbo.sensoryProfileAssessments", "FarmId");
        }
    }
}
