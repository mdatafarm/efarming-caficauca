namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerFieldToFamilyMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.familyUnitMembers", "IsOwner", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.familyUnitMembers", "IsOwner");
        }
    }
}
