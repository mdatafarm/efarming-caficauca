namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_AgentId_In_Agreements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialAgreements", "AgentId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ComercialAgreements", "AgentId");
            AddForeignKey("dbo.ComercialAgreements", "AgentId", "dbo.ComercialAgents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComercialAgreements", "AgentId", "dbo.ComercialAgents");
            DropIndex("dbo.ComercialAgreements", new[] { "AgentId" });
            DropColumn("dbo.ComercialAgreements", "AgentId");
        }
    }
}
