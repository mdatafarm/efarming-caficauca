namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPositionToQualityAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.qualityAttributes", "Position", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.qualityAttributes", "Position");
        }
    }
}
