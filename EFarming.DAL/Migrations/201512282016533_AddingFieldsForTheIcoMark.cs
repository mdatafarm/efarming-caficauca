namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFieldsForTheIcoMark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialSellers", "Code", c => c.Int(nullable: false));
            AddColumn("dbo.countries", "code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.countries", "code");
            DropColumn("dbo.ComercialSellers", "Code");
        }
    }
}
