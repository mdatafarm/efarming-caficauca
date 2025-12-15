namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDepartmentAndMunicipality : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Municipalities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 32),
                        DepartmentId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId);

        }

        public override void Down()
        {
            DropIndex("dbo.Municipalities", new[] { "DepartmentId" });
            DropForeignKey("dbo.Municipalities", "DepartmentId", "dbo.Departments");
            DropTable("dbo.Municipalities");
            DropTable("dbo.Departments");
        }
    }
}
