namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameOfComercialTables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Agents", newName: "ComercialAgents");
            RenameTable(name: "dbo.Clients", newName: "ComercialClients");
            RenameTable(name: "dbo.Agreements", newName: "ComercialAgreements");
            RenameTable(name: "dbo.Sellers", newName: "ComercialSellers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ComercialSellers", newName: "Sellers");
            RenameTable(name: "dbo.ComercialAgreements", newName: "Agreements");
            RenameTable(name: "dbo.ComercialClients", newName: "Clients");
            RenameTable(name: "dbo.ComercialAgents", newName: "Agents");
        }
    }
}
