namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInfrastructureName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.productivities", "InfrastructureHectares", c => c.Double(nullable: false));
            DropColumn("dbo.productivities", "InfraestructureHectares");
        }
        
        public override void Down()
        {
            AddColumn("dbo.productivities", "InfraestructureHectares", c => c.Double(nullable: false));
            DropColumn("dbo.productivities", "InfrastructureHectares");
        }
    }
}
