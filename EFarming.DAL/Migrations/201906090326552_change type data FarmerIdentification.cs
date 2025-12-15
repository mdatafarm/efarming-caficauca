namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypedataFarmerIdentification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.fertilizers", "Identification", c => c.String());
            DropColumn("dbo.fertilizers", "FarmerIdentification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.fertilizers", "FarmerIdentification", c => c.Int(nullable: false));
            DropColumn("dbo.fertilizers", "Identification");
        }
    }
}
