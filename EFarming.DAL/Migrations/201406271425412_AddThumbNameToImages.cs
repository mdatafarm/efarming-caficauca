namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThumbNameToImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ThumbName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ThumbName");
        }
    }
}
