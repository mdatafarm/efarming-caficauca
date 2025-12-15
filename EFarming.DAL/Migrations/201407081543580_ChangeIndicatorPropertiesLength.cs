namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIndicatorPropertiesLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.indicators", "Name", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.indicators", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.indicators", "Description", c => c.String(maxLength: 32));
            AlterColumn("dbo.indicators", "Name", c => c.String(nullable: false, maxLength: 16));
        }
    }
}
