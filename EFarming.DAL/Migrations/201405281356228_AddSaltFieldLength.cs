namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSaltFieldLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Salt", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Salt", c => c.String());
        }
    }
}
