namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addestimetedproductionmanualfieldtoPlantationsentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.plantations", "EstimatedProductionManual", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.plantations", "EstimatedProductionManual");
        }
    }
}
