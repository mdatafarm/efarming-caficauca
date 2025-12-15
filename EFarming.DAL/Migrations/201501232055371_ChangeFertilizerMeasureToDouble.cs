namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFertilizerMeasureToDouble : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.fertilizers", "Measure");
            AddColumn("dbo.fertilizers", "Measure", c => c.Double(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.fertilizers", "Measure");
            AddColumn("dbo.fertilizers", "Measure", c => c.String(nullable: false, maxLength: 16));
        }
    }
}
