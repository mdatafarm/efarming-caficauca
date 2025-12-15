namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_PricesDate_Format_From_Date_To_String : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComercialAgreements", "PriceDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComercialAgreements", "PriceDate", c => c.DateTime());
        }
    }
}
