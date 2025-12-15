namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicNameWithMoreCharacters1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SustainabilityContactLocations", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.SustainabilityContactTopics", "Name", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SustainabilityContactTopics", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.SustainabilityContactLocations", "Name", c => c.String(maxLength: 250));
        }
    }
}
