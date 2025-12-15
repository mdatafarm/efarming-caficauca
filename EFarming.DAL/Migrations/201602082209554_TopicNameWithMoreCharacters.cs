namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicNameWithMoreCharacters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SustainabilityContactLocations", "Name", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SustainabilityContactLocations", "Name", c => c.String(maxLength: 50));
        }
    }
}
