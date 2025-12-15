namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCoffeeAreaField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "coffeeArea", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productivities", "coffeeArea");
        }
    }
}
