namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCoffeeTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoffeeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identifier = c.Int(nullable: false),
                        ProductName = c.String(),
                        Type = c.String(),
                        Category = c.String(),
                        AccumulationFactor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CoffeeTypes");
        }
    }
}
