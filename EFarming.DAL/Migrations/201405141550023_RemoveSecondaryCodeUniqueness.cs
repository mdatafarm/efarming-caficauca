namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSecondaryCodeUniqueness : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.farms", "FarmSecondaryCodeIndex");
            CreateIndex("dbo.farms", "SecondaryCode", unique: false, name: "FarmSecondaryCodeIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.farms", "FarmSecondaryCodeIndex");
            CreateIndex("dbo.farms", "SecondaryCode", unique: true, name: "FarmSecondaryCodeIndex");
        }
    }
}
