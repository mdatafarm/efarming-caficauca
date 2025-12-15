namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContractIdReference_For_Lots : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialLots", "AgreementId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ComercialLots", "AgreementId");
            AddForeignKey("dbo.ComercialLots", "AgreementId", "dbo.ComercialAgreements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComercialLots", "AgreementId", "dbo.ComercialAgreements");
            DropIndex("dbo.ComercialLots", new[] { "AgreementId" });
            DropColumn("dbo.ComercialLots", "AgreementId");
        }
    }
}
