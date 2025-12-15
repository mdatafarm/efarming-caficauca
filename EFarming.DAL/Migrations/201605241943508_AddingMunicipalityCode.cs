namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMunicipalityCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.municipalities", "Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.municipalities", "Code");
        }
    }
}
