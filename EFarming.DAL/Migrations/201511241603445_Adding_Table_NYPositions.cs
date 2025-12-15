namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Table_NYPositions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComercialNYPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        month = c.Int(nullable: false),
                        position = c.String(maxLength: 10),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ComercialNYPositions");
        }
    }
}
