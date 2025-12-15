namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingBasekgType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.invoices", "BaseKg", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.invoices", "BaseKg", c => c.Int(nullable: false));
        }
    }
}
