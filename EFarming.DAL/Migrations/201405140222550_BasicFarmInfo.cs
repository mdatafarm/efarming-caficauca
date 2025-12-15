namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicFarmInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.farms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimaryCode = c.String(nullable: false, maxLength: 16),
                        SecondaryCode = c.String(nullable: false, maxLength: 16),
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            CreateIndex("dbo.farms", "PrimaryCode", unique: true, name: "FarmPrimaryCodeIndex");
            CreateIndex("dbo.farms", "SecondaryCode", unique: true, name: "FarmSecondaryCodeIndex");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.farms", "FarmPrimaryCodeIndex");
            DropIndex("dbo.farms", "FarmSecondaryCodeIndex");
            DropTable("dbo.farms");
        }
    }
}
