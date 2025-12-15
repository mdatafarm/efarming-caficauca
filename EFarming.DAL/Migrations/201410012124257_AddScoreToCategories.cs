namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreToCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.categories", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.categories", "Score");
        }
    }
}
