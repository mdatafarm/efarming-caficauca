namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveValueFieldFromOptionAttribute : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.optionAttributes", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.optionAttributes", "Value", c => c.String(nullable: false, maxLength: 64));
        }
    }
}
