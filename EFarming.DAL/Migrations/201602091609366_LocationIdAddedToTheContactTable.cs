namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationIdAddedToTheContactTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SustainabilityContacts", new[] { "Location_Id" });
            RenameColumn(table: "dbo.SustainabilityContacts", name: "Location_Id", newName: "LocationId");
            AlterColumn("dbo.SustainabilityContacts", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.SustainabilityContacts", "LocationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SustainabilityContacts", new[] { "LocationId" });
            AlterColumn("dbo.SustainabilityContacts", "LocationId", c => c.Int());
            RenameColumn(table: "dbo.SustainabilityContacts", name: "LocationId", newName: "Location_Id");
            CreateIndex("dbo.SustainabilityContacts", "Location_Id");
        }
    }
}
