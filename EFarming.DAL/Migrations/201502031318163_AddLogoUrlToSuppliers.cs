namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogoUrlToSuppliers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.suppliers", "LogoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.suppliers", "LogoUrl");
        }
    }
}
