namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_FarmStatusId_Field : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.farms", name: "FarmStatus_Id", newName: "FarmStatusId");
            RenameIndex(table: "dbo.farms", name: "IX_FarmStatus_Id", newName: "IX_FarmStatusId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.farms", name: "IX_FarmStatusId", newName: "IX_FarmStatus_Id");
            RenameColumn(table: "dbo.farms", name: "FarmStatusId", newName: "FarmStatus_Id");
        }
    }
}
