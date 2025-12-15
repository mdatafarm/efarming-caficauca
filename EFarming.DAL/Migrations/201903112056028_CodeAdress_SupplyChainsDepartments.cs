namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeAdress_SupplyChainsDepartments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Code", c => c.Int());
            AddColumn("dbo.supplyChains", "Code", c => c.Int());
            AddColumn("dbo.supplyChains", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.supplyChains", "Address");
            DropColumn("dbo.supplyChains", "Code");
            DropColumn("dbo.Departments", "Code");
        }
    }
}
