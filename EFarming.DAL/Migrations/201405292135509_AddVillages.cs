namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVillages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.villages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        MunicipalityId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Municipalities", t => t.MunicipalityId)
                .Index(t => t.MunicipalityId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.villages", new[] { "MunicipalityId" });
            DropForeignKey("dbo.villages", "MunicipalityId", "dbo.Municipalities");
            DropTable("dbo.villages");
        }
    }
}
