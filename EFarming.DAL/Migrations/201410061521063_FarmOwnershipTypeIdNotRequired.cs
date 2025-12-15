namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FarmOwnershipTypeIdNotRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.farms", new[] { "OwnershipTypeId" });
            AlterColumn("dbo.farms", "OwnershipTypeId", c => c.Guid());
            CreateIndex("dbo.farms", "OwnershipTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.farms", new[] { "OwnershipTypeId" });
            AlterColumn("dbo.farms", "OwnershipTypeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.farms", "OwnershipTypeId");
        }
    }
}
