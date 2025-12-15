namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOpenTextEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.openTextAttributes", "Id", "dbo.qualityAttributes");
            DropIndex("dbo.openTextAttributes", new[] { "Id" });
            DropTable("dbo.openTextAttributes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.openTextAttributes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.openTextAttributes", "Id");
            AddForeignKey("dbo.openTextAttributes", "Id", "dbo.qualityAttributes", "Id");
        }
    }
}
