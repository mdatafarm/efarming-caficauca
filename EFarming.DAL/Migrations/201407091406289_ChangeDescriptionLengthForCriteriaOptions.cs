namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDescriptionLengthForCriteriaOptions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.criteriaOptions", "Description", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.criteriaOptions", "Description", c => c.String(nullable: false, maxLength: 32));
        }
    }
}
