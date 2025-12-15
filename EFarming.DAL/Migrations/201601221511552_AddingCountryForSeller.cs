namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCountryForSeller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialSellers", "Country", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialSellers", "Country");
        }
    }
}
