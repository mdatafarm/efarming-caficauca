namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSomeFieldsToSeller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialSellers", "Address", c => c.String(maxLength: 25));
            AddColumn("dbo.ComercialSellers", "ZipCode", c => c.Int());
            AddColumn("dbo.ComercialSellers", "City", c => c.String(maxLength: 25));
            AddColumn("dbo.ComercialSellers", "Telephone", c => c.String(maxLength: 25));
            AddColumn("dbo.ComercialSellers", "email", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialSellers", "email");
            DropColumn("dbo.ComercialSellers", "Telephone");
            DropColumn("dbo.ComercialSellers", "City");
            DropColumn("dbo.ComercialSellers", "ZipCode");
            DropColumn("dbo.ComercialSellers", "Address");
        }
    }
}
