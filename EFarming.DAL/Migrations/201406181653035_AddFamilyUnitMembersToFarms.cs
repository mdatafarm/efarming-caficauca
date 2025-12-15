namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFamilyUnitMembersToFarms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.familyUnitMembers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 32),
                        LastName = c.String(nullable: false, maxLength: 32),
                        Age = c.Int(),
                        Identification = c.String(maxLength: 16),
                        Education = c.String(maxLength: 32),
                        Relationship = c.String(nullable: false, maxLength: 32),
                        MaritalStatus = c.String(nullable: false, maxLength: 32),
                        FarmId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.FarmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.familyUnitMembers", "FarmId", "dbo.farms");
            DropIndex("dbo.familyUnitMembers", new[] { "FarmId" });
            DropTable("dbo.familyUnitMembers");
        }
    }
}
