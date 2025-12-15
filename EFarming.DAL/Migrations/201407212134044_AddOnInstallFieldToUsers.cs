namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddOnInstallFieldToUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "OnInstall", c => c.Boolean(nullable: false, defaultValue: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "OnInstall");
        }
    }
}
