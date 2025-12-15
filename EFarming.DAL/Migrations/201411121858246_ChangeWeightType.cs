namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeWeightType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.invoices", "Weight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.invoices", "Weight", c => c.Int(nullable: false));
        }
    }
}
