namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVillageAndMunicipalityToMicrolot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Microlots", "MunicipalityId", c => c.Guid());
            AddColumn("dbo.Microlots", "VillageId", c => c.Guid());
            CreateIndex("dbo.Microlots", "MunicipalityId");
            CreateIndex("dbo.Microlots", "VillageId");
            AddForeignKey("dbo.Microlots", "MunicipalityId", "dbo.municipalities", "Id");
            AddForeignKey("dbo.Microlots", "VillageId", "dbo.villages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Microlots", "VillageId", "dbo.villages");
            DropForeignKey("dbo.Microlots", "MunicipalityId", "dbo.municipalities");
            DropIndex("dbo.Microlots", new[] { "VillageId" });
            DropIndex("dbo.Microlots", new[] { "MunicipalityId" });
            DropColumn("dbo.Microlots", "VillageId");
            DropColumn("dbo.Microlots", "MunicipalityId");
        }
    }
}
