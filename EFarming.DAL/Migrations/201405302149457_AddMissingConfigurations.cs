namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMissingConfigurations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FarmSubstatus", newName: "farmSubstatuses");
            AlterColumn("dbo.municipalities", "Name", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.farmSubstatuses", "Name", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.farmSubstatuses", "Name", c => c.String());
            AlterColumn("dbo.municipalities", "Name", c => c.String());
            RenameTable(name: "dbo.farmSubstatuses", newName: "FarmSubstatus");
        }
    }
}
