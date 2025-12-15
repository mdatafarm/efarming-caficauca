namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable_Price_Fields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ComercialAgreements", "PriceLots", c => c.Int());
            AlterColumn("dbo.ComercialAgreements", "PriceDate", c => c.DateTime());
            AlterColumn("dbo.ComercialAgreements", "PriceUSD", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ComercialAgreements", "PriceUSD", c => c.Int(nullable: false));
            AlterColumn("dbo.ComercialAgreements", "PriceDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ComercialAgreements", "PriceLots", c => c.Int(nullable: false));
        }
    }
}
