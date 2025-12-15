namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrincipalAttributeToSetMainPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Principal", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Principal");
        }
    }
}
