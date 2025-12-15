namespace EFarming.DAL.Migrations
{
    using EFarming.Core.AuthenticationModule.AutenticationAggregate;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTasterToSensoryProfileAssessments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.sensoryProfileAssessments", "TasterId", c => c.Guid(nullable: false, defaultValue: User.SYSTEM));
            CreateIndex("dbo.sensoryProfileAssessments", "TasterId");
            AddForeignKey("dbo.sensoryProfileAssessments", "TasterId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.sensoryProfileAssessments", "TasterId", "dbo.Users");
            DropIndex("dbo.sensoryProfileAssessments", new[] { "TasterId" });
            DropColumn("dbo.sensoryProfileAssessments", "TasterId");
        }
    }
}
