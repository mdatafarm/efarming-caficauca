namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClientFieldsAndAgentEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "Address", c => c.String());
            AddColumn("dbo.Clients", "ZipCode", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "City", c => c.String());
            AddColumn("dbo.Clients", "Country", c => c.String());
            AddColumn("dbo.Clients", "Agent_Id", c => c.Guid());
            CreateIndex("dbo.Clients", "Agent_Id");
            AddForeignKey("dbo.Clients", "Agent_Id", "dbo.Agents", "Id");
            DropColumn("dbo.Clients", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "ClientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Clients", "Agent_Id", "dbo.Agents");
            DropIndex("dbo.Clients", new[] { "Agent_Id" });
            DropColumn("dbo.Clients", "Agent_Id");
            DropColumn("dbo.Clients", "Country");
            DropColumn("dbo.Clients", "City");
            DropColumn("dbo.Clients", "ZipCode");
            DropColumn("dbo.Clients", "Address");
            DropTable("dbo.Agents");
        }
    }
}
