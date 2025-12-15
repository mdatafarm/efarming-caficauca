namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFertilizersCalculatorEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AverageExtraction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        N = c.Decimal(precision: 18, scale: 2),
                        P2O5 = c.Decimal(precision: 18, scale: 2),
                        K20 = c.Decimal(precision: 18, scale: 2),
                        CaO = c.Decimal(precision: 18, scale: 2),
                        MgO = c.Decimal(precision: 18, scale: 2),
                        SO4 = c.Decimal(precision: 18, scale: 2),
                        B = c.Decimal(precision: 18, scale: 2),
                        Zn = c.Decimal(precision: 18, scale: 2),
                        Cu = c.Decimal(precision: 18, scale: 2),
                        Fe = c.Decimal(precision: 18, scale: 2),
                        Mn = c.Decimal(precision: 18, scale: 2),
                        Mo = c.Decimal(precision: 18, scale: 2),
                        SiO = c.Decimal(precision: 18, scale: 2),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FertilizerInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        kg = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        N = c.Decimal(precision: 18, scale: 2),
                        P2O5 = c.Decimal(precision: 18, scale: 2),
                        K20 = c.Decimal(precision: 18, scale: 2),
                        CaO = c.Decimal(precision: 18, scale: 2),
                        MgO = c.Decimal(precision: 18, scale: 2),
                        SO4 = c.Decimal(precision: 18, scale: 2),
                        B = c.Decimal(precision: 18, scale: 2),
                        Zn = c.Decimal(precision: 18, scale: 2),
                        Cu = c.Decimal(precision: 18, scale: 2),
                        Fe = c.Decimal(precision: 18, scale: 2),
                        Mn = c.Decimal(precision: 18, scale: 2),
                        Mo = c.Decimal(precision: 18, scale: 2),
                        SiO = c.Decimal(precision: 18, scale: 2),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FertilizerInformation");
            DropTable("dbo.AverageExtraction");
        }
    }
}
