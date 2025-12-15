namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQualityAttributeId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.openTextAttribute", "QualityAttributeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.openTextAttribute", "QualityAttributeId", c => c.Guid(nullable: false));
        }
    }
}
