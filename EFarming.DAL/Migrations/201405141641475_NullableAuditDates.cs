namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableAuditDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.farms", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.farms", "UpdatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.farms", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.farms", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
