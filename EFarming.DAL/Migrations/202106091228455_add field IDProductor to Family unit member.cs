namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfieldIDProductortoFamilyunitmember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.familyUnitMembers", "IDProductor", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.familyUnitMembers", "IDProductor");
        }
    }
}
