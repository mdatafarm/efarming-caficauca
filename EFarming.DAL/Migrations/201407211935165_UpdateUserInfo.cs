namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserInfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 64));
            DropColumn("dbo.Users", "CreateDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
        }
    }
}
