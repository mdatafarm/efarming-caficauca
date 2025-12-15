namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAttributeIdField : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.optionAttributes", name: "AttributeId", newName: "QualityAttributeId");
            RenameIndex(table: "dbo.optionAttributes", name: "IX_AttributeId", newName: "IX_QualityAttributeId");
            DropColumn("dbo.openTextAttributes", "AttributeId");
            DropColumn("dbo.rangeAttributes", "AttributeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.rangeAttributes", "AttributeId", c => c.Guid(nullable: false));
            AddColumn("dbo.openTextAttributes", "AttributeId", c => c.Guid(nullable: false));
            RenameIndex(table: "dbo.optionAttributes", name: "IX_QualityAttributeId", newName: "IX_AttributeId");
            RenameColumn(table: "dbo.optionAttributes", name: "QualityAttributeId", newName: "AttributeId");
        }
    }
}
