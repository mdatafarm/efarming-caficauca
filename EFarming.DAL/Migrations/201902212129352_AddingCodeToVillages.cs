namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCodeToVillages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.villages", "Code", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.villages", "Code");
        }
    }
}
