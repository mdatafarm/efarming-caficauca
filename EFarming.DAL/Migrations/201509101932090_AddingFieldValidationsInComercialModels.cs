namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFieldValidationsInComercialModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComercialAgents", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialAgents", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialClients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialClients", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialClients", "City", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialClients", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialSellers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialSellers", "Footer", c => c.String(nullable: false));
            AlterColumn("dbo.ComercialSellers", "Header", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComercialSellers", "Header", c => c.String());
            AlterColumn("dbo.ComercialSellers", "Footer", c => c.String());
            AlterColumn("dbo.ComercialSellers", "Name", c => c.String());
            AlterColumn("dbo.ComercialClients", "Country", c => c.String());
            AlterColumn("dbo.ComercialClients", "City", c => c.String());
            AlterColumn("dbo.ComercialClients", "Address", c => c.String());
            AlterColumn("dbo.ComercialClients", "Name", c => c.String());
            AlterColumn("dbo.ComercialAgents", "Email", c => c.String());
            AlterColumn("dbo.ComercialAgents", "Name", c => c.String());
        }
    }
}
