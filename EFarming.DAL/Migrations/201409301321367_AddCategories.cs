namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.indicators", "CategoryId", c => c.Guid());
            CreateIndex("dbo.indicators", "CategoryId");
            AddForeignKey("dbo.indicators", "CategoryId", "dbo.categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.indicators", "CategoryId", "dbo.categories");
            DropIndex("dbo.indicators", new[] { "CategoryId" });
            DropColumn("dbo.indicators", "CategoryId");
            DropTable("dbo.categories");
        }
    }
}
