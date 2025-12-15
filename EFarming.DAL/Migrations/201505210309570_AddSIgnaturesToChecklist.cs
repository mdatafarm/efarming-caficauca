namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSIgnaturesToChecklist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checklists", "TechnicianSignatureUrl", c => c.String());
            AddColumn("dbo.Checklists", "FarmerSignatureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checklists", "FarmerSignatureUrl");
            DropColumn("dbo.Checklists", "TechnicianSignatureUrl");
        }
    }
}
