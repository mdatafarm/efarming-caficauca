namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Short_Field_In_MoreInformation_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComercialMoreInformation", "Short", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComercialMoreInformation", "Short");
        }
    }
}
