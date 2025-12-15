namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRelationReference : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ComercialReferencesRelationShip", "Id");
            AddForeignKey("dbo.ComercialReferencesRelationShip", "Id", "dbo.ComercialLots", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComercialReferencesRelationShip", "Id", "dbo.ComercialLots");
            DropIndex("dbo.ComercialReferencesRelationShip", new[] { "Id" });
        }
    }
}
