namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedAtFieldToTrackDeletions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cooperatives", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.criteria", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.criteriaOptions", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.impactAssessments", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.farms", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.familyUnitMembers", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.farmSubstatuses", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.farmStatuses", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.fertilizers", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Images", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.farmOtherActivities", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.OtherActivities", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.ownershipTypes", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.productivities", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.plantations", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.floweringPeriods", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.FloweringPeriodQualifications", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.PlantationStatuses", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.PlantationTypes", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.PlantationVarieties", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.soilAnalysis", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.SoilTypes", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.villages", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.municipalities", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Departments", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.workers", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.indicators", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Roles", "DeletedAt", c => c.DateTime());
            AddColumn("dbo.Users", "DeletedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeletedAt");
            DropColumn("dbo.Roles", "DeletedAt");
            DropColumn("dbo.indicators", "DeletedAt");
            DropColumn("dbo.workers", "DeletedAt");
            DropColumn("dbo.Departments", "DeletedAt");
            DropColumn("dbo.municipalities", "DeletedAt");
            DropColumn("dbo.villages", "DeletedAt");
            DropColumn("dbo.SoilTypes", "DeletedAt");
            DropColumn("dbo.soilAnalysis", "DeletedAt");
            DropColumn("dbo.PlantationVarieties", "DeletedAt");
            DropColumn("dbo.PlantationTypes", "DeletedAt");
            DropColumn("dbo.PlantationStatuses", "DeletedAt");
            DropColumn("dbo.FloweringPeriodQualifications", "DeletedAt");
            DropColumn("dbo.floweringPeriods", "DeletedAt");
            DropColumn("dbo.plantations", "DeletedAt");
            DropColumn("dbo.productivities", "DeletedAt");
            DropColumn("dbo.ownershipTypes", "DeletedAt");
            DropColumn("dbo.OtherActivities", "DeletedAt");
            DropColumn("dbo.farmOtherActivities", "DeletedAt");
            DropColumn("dbo.Images", "DeletedAt");
            DropColumn("dbo.fertilizers", "DeletedAt");
            DropColumn("dbo.farmStatuses", "DeletedAt");
            DropColumn("dbo.farmSubstatuses", "DeletedAt");
            DropColumn("dbo.familyUnitMembers", "DeletedAt");
            DropColumn("dbo.farms", "DeletedAt");
            DropColumn("dbo.impactAssessments", "DeletedAt");
            DropColumn("dbo.criteriaOptions", "DeletedAt");
            DropColumn("dbo.criteria", "DeletedAt");
            DropColumn("dbo.cooperatives", "DeletedAt");
        }
    }
}
