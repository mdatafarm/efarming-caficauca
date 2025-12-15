namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClientIdFieldToAgents : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Agents", new[] { "Client_Id" });
            RenameColumn(table: "dbo.Agents", name: "Client_Id", newName: "ClientId");
            AlterColumn("dbo.Agents", "ClientId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Agents", "ClientId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Agents", new[] { "ClientId" });
            AlterColumn("dbo.Agents", "ClientId", c => c.Guid());
            RenameColumn(table: "dbo.Agents", name: "ClientId", newName: "Client_Id");
            CreateIndex("dbo.Agents", "Client_Id");
        }
    }
}
