namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_PhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.familyUnitMembers", "PhoneNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.familyUnitMembers", "PhoneNumber");
        }
    }
}
