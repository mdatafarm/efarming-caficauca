namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCriteriaDescriptionLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.criteria", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.criteria", "Description", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
