namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Fixation_Fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialAgreements", "PriceType", c => c.String());
            AddColumn("dbo.ComercialAgreements", "PriceDifferential", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ComercialAgreements", "Fixation", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ComercialAgreements", "FixationDate", c => c.DateTime());
            DropColumn("dbo.ComercialAgreements", "PriceUSD");
            DropColumn("dbo.ComercialAgreements", "FinalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComercialAgreements", "FinalPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ComercialAgreements", "PriceUSD", c => c.Int());
            DropColumn("dbo.ComercialAgreements", "FixationDate");
            DropColumn("dbo.ComercialAgreements", "Fixation");
            DropColumn("dbo.ComercialAgreements", "PriceDifferential");
            DropColumn("dbo.ComercialAgreements", "PriceType");
        }
    }
}
