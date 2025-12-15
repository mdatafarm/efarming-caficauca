namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAverageAgeProducrivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "averageAge", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.productivities", "averageAge");
        }
    }
}
