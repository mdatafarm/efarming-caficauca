namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FinalPrice_Change_PriceLots : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialAgreements", "LotsNumber", c => c.Int());
            AddColumn("dbo.ComercialAgreements", "FinalPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.ComercialAgreements", "PriceLots");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ComercialAgreements", "PriceLots", c => c.Int());
            DropColumn("dbo.ComercialAgreements", "FinalPrice");
            DropColumn("dbo.ComercialAgreements", "LotsNumber");
        }
    }
}
