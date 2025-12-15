namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpenTextAttribute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.openTextAttribute",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Boolean(defaultValue: false),
                        QualityAttributeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.qualityAttributes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.openTextAttribute", "Id", "dbo.qualityAttributes");
            DropIndex("dbo.openTextAttribute", new[] { "Id" });
            DropTable("dbo.openTextAttribute");
        }
    }
}
