namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesForMerchandiseDescription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComercialMDOrigin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CooperativeId = c.Int(nullable: false),
                        ClientId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComercialClients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ComercialMDType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClientId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComercialClients", t => t.ClientId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComercialMDType", "ClientId", "dbo.ComercialClients");
            DropForeignKey("dbo.ComercialMDOrigin", "ClientId", "dbo.ComercialClients");
            DropIndex("dbo.ComercialMDType", new[] { "ClientId" });
            DropIndex("dbo.ComercialMDOrigin", new[] { "ClientId" });
            DropTable("dbo.ComercialMDType");
            DropTable("dbo.ComercialMDOrigin");
        }
    }
}
