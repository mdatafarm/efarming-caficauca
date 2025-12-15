namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefTypeForTheLotReferences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialReferences", "RefType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialReferences", "RefType");
        }
    }
}
