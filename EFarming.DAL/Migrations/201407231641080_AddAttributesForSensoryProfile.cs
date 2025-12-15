namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributesForSensoryProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.attributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 128),
                        TypeOf = c.String(nullable: false, maxLength: 16),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.openTextAttributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        AttributeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.attributes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.optionAttributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 64),
                        Value = c.String(nullable: false, maxLength: 64),
                        AttributeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.attributes", t => t.AttributeId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.rangeAttributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MinVal = c.Double(nullable: false),
                        MaxVal = c.Double(nullable: false),
                        Step = c.Double(nullable: false),
                        AttributeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.attributes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.rangeAttributes", "Id", "dbo.attributes");
            DropForeignKey("dbo.optionAttributes", "AttributeId", "dbo.attributes");
            DropForeignKey("dbo.openTextAttributes", "Id", "dbo.attributes");
            DropIndex("dbo.rangeAttributes", new[] { "Id" });
            DropIndex("dbo.optionAttributes", new[] { "AttributeId" });
            DropIndex("dbo.openTextAttributes", new[] { "Id" });
            DropTable("dbo.rangeAttributes");
            DropTable("dbo.optionAttributes");
            DropTable("dbo.openTextAttributes");
            DropTable("dbo.attributes");
        }
    }
}
