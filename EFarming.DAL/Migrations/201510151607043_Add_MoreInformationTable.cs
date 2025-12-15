namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_MoreInformationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComercialMoreInformation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InformationType = c.String(nullable: false),
                        ClientId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Text = c.String(),
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
            DropForeignKey("dbo.ComercialMoreInformation", "ClientId", "dbo.ComercialClients");
            DropIndex("dbo.ComercialMoreInformation", new[] { "ClientId" });
            DropTable("dbo.ComercialMoreInformation");
        }
    }
}
