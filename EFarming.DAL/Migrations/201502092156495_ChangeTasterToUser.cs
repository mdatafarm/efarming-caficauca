namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTasterToUser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.sensoryProfileAssessments", name: "TasterId", newName: "UserId");
            RenameIndex(table: "dbo.sensoryProfileAssessments", name: "IX_TasterId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.sensoryProfileAssessments", name: "IX_UserId", newName: "IX_TasterId");
            RenameColumn(table: "dbo.sensoryProfileAssessments", name: "UserId", newName: "TasterId");
        }
    }
}
