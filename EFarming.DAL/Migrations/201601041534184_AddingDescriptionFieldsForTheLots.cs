namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDescriptionFieldsForTheLots : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialLots", "IcoMark", c => c.String(maxLength: 15));
            AddColumn("dbo.ComercialLots", "MerchandiseDescription", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialLots", "MerchandiseDescription");
            DropColumn("dbo.ComercialLots", "IcoMark");
        }
    }
}
