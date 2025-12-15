namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingFloweringQualificationPeriod : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.floweringPeriods", "FloweringPeriodQualificationId", "dbo.FloweringPeriodQualifications");
            DropIndex("dbo.floweringPeriods", new[] { "FloweringPeriodQualificationId" });
            DropColumn("dbo.floweringPeriods", "FloweringPeriodQualificationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.floweringPeriods", "FloweringPeriodQualificationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.floweringPeriods", "FloweringPeriodQualificationId");
            AddForeignKey("dbo.floweringPeriods", "FloweringPeriodQualificationId", "dbo.FloweringPeriodQualifications", "Id");
        }
    }
}
