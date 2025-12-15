namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypedatainfamilyunitmember : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.familyUnitMembers", "Education", c => c.String());
            AlterColumn("dbo.familyUnitMembers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.familyUnitMembers", "PhoneNumber", c => c.Long());
            AlterColumn("dbo.familyUnitMembers", "Education", c => c.Int());
        }
    }
}
