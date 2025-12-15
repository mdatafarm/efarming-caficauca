namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTablesForTASQAssessment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TASQModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        AssessmentTemplateId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.assessmentTemplates", t => t.AssessmentTemplateId)
                .Index(t => t.AssessmentTemplateId);
            
            CreateTable(
                "dbo.TASQSubModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ModuleId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TASQModules", t => t.ModuleId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.TASQAssessments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        FarmId = c.Guid(nullable: false),
                        AssessmentTemplateId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.assessmentTemplates", t => t.AssessmentTemplateId)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId)
                .Index(t => t.AssessmentTemplateId);
            
            CreateTable(
                "dbo.TASQAssessmentAnswers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AssessmentId = c.Guid(nullable: false),
                        CriteriaId = c.Int(nullable: false),
                        Value = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                        TASQAssessment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TASQCriterias", t => t.CriteriaId)
                .ForeignKey("dbo.TASQAssessments", t => t.TASQAssessment_Id)
                .Index(t => t.CriteriaId)
                .Index(t => t.TASQAssessment_Id);
            
            CreateTable(
                "dbo.TASQCriterias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        SubModuleId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TASQSubModules", t => t.SubModuleId)
                .Index(t => t.SubModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TASQAssessmentAnswers", "TASQAssessment_Id", "dbo.TASQAssessments");
            DropForeignKey("dbo.TASQAssessmentAnswers", "CriteriaId", "dbo.TASQCriterias");
            DropForeignKey("dbo.TASQCriterias", "SubModuleId", "dbo.TASQSubModules");
            DropForeignKey("dbo.TASQAssessments", "FarmId", "dbo.farms");
            DropForeignKey("dbo.TASQAssessments", "AssessmentTemplateId", "dbo.assessmentTemplates");
            DropForeignKey("dbo.TASQSubModules", "ModuleId", "dbo.TASQModules");
            DropForeignKey("dbo.TASQModules", "AssessmentTemplateId", "dbo.assessmentTemplates");
            DropIndex("dbo.TASQCriterias", new[] { "SubModuleId" });
            DropIndex("dbo.TASQAssessmentAnswers", new[] { "TASQAssessment_Id" });
            DropIndex("dbo.TASQAssessmentAnswers", new[] { "CriteriaId" });
            DropIndex("dbo.TASQAssessments", new[] { "AssessmentTemplateId" });
            DropIndex("dbo.TASQAssessments", new[] { "FarmId" });
            DropIndex("dbo.TASQSubModules", new[] { "ModuleId" });
            DropIndex("dbo.TASQModules", new[] { "AssessmentTemplateId" });
            DropTable("dbo.TASQCriterias");
            DropTable("dbo.TASQAssessmentAnswers");
            DropTable("dbo.TASQAssessments");
            DropTable("dbo.TASQSubModules");
            DropTable("dbo.TASQModules");
        }
    }
}
