namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletingEndDateForFloweringPeriods : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.floweringPeriods", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.floweringPeriods", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
