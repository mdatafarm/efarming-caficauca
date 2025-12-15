namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSecondaryCodeRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.farms", "SecondaryCode", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.farms", "SecondaryCode", c => c.String(nullable: false, maxLength: 16));
        }
    }
}
