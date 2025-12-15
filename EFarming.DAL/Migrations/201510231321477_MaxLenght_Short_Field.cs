namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLenght_Short_Field : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComercialMoreInformation", "Short", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComercialMoreInformation", "Short", c => c.String());
        }
    }
}
