namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_YourRef_Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialAgreements", "YourRef", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialAgreements", "YourRef");
        }
    }
}
