namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAttributesTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.attributes", newName: "qualityAttributes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.qualityAttributes", newName: "attributes");
        }
    }
}
