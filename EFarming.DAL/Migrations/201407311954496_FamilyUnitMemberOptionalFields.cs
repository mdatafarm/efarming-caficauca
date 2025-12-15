namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FamilyUnitMemberOptionalFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.familyUnitMembers", "FirstName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.familyUnitMembers", "LastName", c => c.String(maxLength: 32));
            AlterColumn("dbo.familyUnitMembers", "Relationship", c => c.String(maxLength: 32));
            AlterColumn("dbo.familyUnitMembers", "MaritalStatus", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.familyUnitMembers", "MaritalStatus", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.familyUnitMembers", "Relationship", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.familyUnitMembers", "LastName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.familyUnitMembers", "FirstName", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
