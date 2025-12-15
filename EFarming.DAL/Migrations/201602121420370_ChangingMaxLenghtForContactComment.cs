namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingMaxLenghtForContactComment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SustainabilityContacts", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SustainabilityContacts", "Comment", c => c.String(maxLength: 500));
        }
    }
}
