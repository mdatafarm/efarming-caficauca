namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changing_PhoneNumber_To_Long : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.familyUnitMembers", "PhoneNumber", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.familyUnitMembers", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
