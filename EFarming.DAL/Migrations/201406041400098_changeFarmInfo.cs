namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    
    public partial class changeFarmInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.farms", "Code", c => c.String(nullable: false, maxLength: 16));
            AddColumn("dbo.farms", "GeoLocation", c => c.Geography(nullable: false));
            AddColumn("dbo.farms", "VillageId", c => c.Guid(nullable: false));
            AddColumn("dbo.farms", "FarmSubstatusId", c => c.Guid(nullable: false));
            AddColumn("dbo.farms", "CooperativeId", c => c.Guid(nullable: false));
            AddColumn("dbo.farms", "OwnershipTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.farms", "VillageId");
            CreateIndex("dbo.farms", "FarmSubstatusId");
            CreateIndex("dbo.farms", "CooperativeId");
            CreateIndex("dbo.farms", "OwnershipTypeId");
            DropIndex("dbo.farms", "FarmPrimaryCodeIndex");
            DropIndex("dbo.farms", "FarmSecondaryCodeIndex");
            AddForeignKey("dbo.farms", "CooperativeId", "dbo.cooperatives", "Id");
            AddForeignKey("dbo.farms", "FarmSubstatusId", "dbo.farmSubstatuses", "Id");
            AddForeignKey("dbo.farms", "OwnershipTypeId", "dbo.ownershipTypes", "Id");
            AddForeignKey("dbo.farms", "VillageId", "dbo.villages", "Id");
            DropColumn("dbo.farms", "PrimaryCode");
            DropColumn("dbo.farms", "SecondaryCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.farms", "SecondaryCode", c => c.String(maxLength: 16));
            AddColumn("dbo.farms", "PrimaryCode", c => c.String(nullable: false, maxLength: 16));
            DropForeignKey("dbo.farms", "VillageId", "dbo.villages");
            DropForeignKey("dbo.farms", "OwnershipTypeId", "dbo.ownershipTypes");
            DropForeignKey("dbo.farms", "FarmSubstatusId", "dbo.farmSubstatuses");
            DropForeignKey("dbo.farms", "CooperativeId", "dbo.cooperatives");
            DropIndex("dbo.farms", new[] { "OwnershipTypeId" });
            DropIndex("dbo.farms", new[] { "CooperativeId" });
            DropIndex("dbo.farms", new[] { "FarmSubstatusId" });
            DropIndex("dbo.farms", new[] { "VillageId" });
            DropColumn("dbo.farms", "OwnershipTypeId");
            DropColumn("dbo.farms", "CooperativeId");
            DropColumn("dbo.farms", "FarmSubstatusId");
            DropColumn("dbo.farms", "VillageId");
            DropColumn("dbo.farms", "GeoLocation");
            DropColumn("dbo.farms", "Code");
            CreateIndex("dbo.farms", "PrimaryCode", unique: true, name: "FarmPrimaryCodeIndex");
            CreateIndex("dbo.farms", "SecondaryCode", unique: true, name: "FarmSecondaryCodeIndex");
        }
    }
}
