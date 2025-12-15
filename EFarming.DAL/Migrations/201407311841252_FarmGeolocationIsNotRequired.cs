namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FarmGeolocationIsNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.farms", "GeoLocation", c => c.Geography());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.farms", "GeoLocation", c => c.Geography(nullable: false));
        }
    }
}
