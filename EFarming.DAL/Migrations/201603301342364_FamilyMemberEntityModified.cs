namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FamilyMemberEntityModified : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.familyUnitMembers", "Age", c => c.DateTime());
            AlterColumn("dbo.familyUnitMembers", "Education", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.familyUnitMembers", "Education", c => c.String(maxLength: 32));
            AlterColumn("dbo.familyUnitMembers", "Age", c => c.Int());
        }
    }
}
