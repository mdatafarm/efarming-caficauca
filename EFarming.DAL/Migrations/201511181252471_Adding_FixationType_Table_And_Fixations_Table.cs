namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_FixationType_Table_And_Fixations_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComercialFixations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FixationDate = c.DateTime(nullable: false),
                        FixationLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fixed = c.Boolean(nullable: false),
                        FixationTypeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComercialAgreements", t => t.Id)
                .ForeignKey("dbo.ComercialFixationTypes", t => t.FixationTypeId)
                .Index(t => t.Id)
                .Index(t => t.FixationTypeId);
            
            CreateTable(
                "dbo.ComercialFixationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComercialFixations", "FixationTypeId", "dbo.ComercialFixationTypes");
            DropForeignKey("dbo.ComercialFixations", "Id", "dbo.ComercialAgreements");
            DropIndex("dbo.ComercialFixations", new[] { "FixationTypeId" });
            DropIndex("dbo.ComercialFixations", new[] { "Id" });
            DropTable("dbo.ComercialFixationTypes");
            DropTable("dbo.ComercialFixations");
        }
    }
}
