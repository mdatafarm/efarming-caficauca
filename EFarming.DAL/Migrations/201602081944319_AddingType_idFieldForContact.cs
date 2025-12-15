namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingType_idFieldForContact : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SustainabilityContacts", new[] { "Type_Id" });
            DropIndex("dbo.SustainabilityContacts", new[] { "User_Id" });
            RenameColumn(table: "dbo.SustainabilityContacts", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.SustainabilityContacts", name: "Type_Id", newName: "TypeId");
            AlterColumn("dbo.SustainabilityContacts", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.SustainabilityContacts", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SustainabilityContacts", "TypeId");
            CreateIndex("dbo.SustainabilityContacts", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SustainabilityContacts", new[] { "UserId" });
            DropIndex("dbo.SustainabilityContacts", new[] { "TypeId" });
            AlterColumn("dbo.SustainabilityContacts", "UserId", c => c.Guid());
            AlterColumn("dbo.SustainabilityContacts", "TypeId", c => c.Int());
            RenameColumn(table: "dbo.SustainabilityContacts", name: "TypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.SustainabilityContacts", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.SustainabilityContacts", "User_Id");
            CreateIndex("dbo.SustainabilityContacts", "Type_Id");
        }
    }
}
