namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChecklistInformation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.criteria", "RequirementId", "dbo.Requirements");
            DropIndex("dbo.criteria", new[] { "RequirementId" });
            CreateTable(
                "dbo.Checklists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Observaciones = c.String(),
                        UserId = c.Guid(nullable: false),
                        FarmId = c.Guid(nullable: false),
                        AlmacenamientoId = c.Guid(nullable: false),
                        DespulpadoId = c.Guid(nullable: false),
                        FermentacionId = c.Guid(nullable: false),
                        InfraestucturaId = c.Guid(nullable: false),
                        MantenimientoId = c.Guid(nullable: false),
                        RecoleccionId = c.Guid(nullable: false),
                        SecadoId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.farms", t => t.FarmId)
                .Index(t => t.UserId)
                .Index(t => t.FarmId);
            
            CreateTable(
                "dbo.Almacenamientoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AlmacenamientoExclusivoCafe = c.Boolean(nullable: false),
                        TipoEmpaqueCafeNespresso = c.String(),
                        AlmacenaProductosContaminantes = c.Boolean(nullable: false),
                        CondicionesMinimas = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Despulpadoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SistemaClasificacion = c.Boolean(nullable: false),
                        UtilizaAgua = c.Boolean(nullable: false),
                        PasaFermentacion = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Fermentacions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TipoBeneficiadero = c.String(),
                        TiempoFermentacion = c.Int(nullable: false),
                        SistemaFermentacion = c.String(),
                        CriterioIdentificacionLavado = c.String(),
                        MaterialTanque = c.String(),
                        LavaDespuesFermentado = c.Boolean(nullable: false),
                        JuntaEnCochadas = c.Boolean(nullable: false),
                        CuantasCochadas = c.Int(nullable: false),
                        CuantosDiasCochadas = c.Int(nullable: false),
                        FuenteAgua = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Infraestructuras",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Tolva = c.String(),
                        Despulpadora = c.String(),
                        Desmucilaginador = c.String(),
                        TanqueFermentacion = c.String(),
                        CanalesCarreteo = c.String(),
                        PisoBeneficiadero = c.String(),
                        SistemaSecadoMecanico = c.String(),
                        SistemaSecadoSolar = c.String(),
                        BodegaAlmacenamiento = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Mantenimientoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrganizacionLimpiezaBeneficiadero = c.String(),
                        PuedeContaminarse = c.Boolean(nullable: false),
                        OrganizacionLimpiezaEquiposBeneficiadero = c.String(),
                        FrecuenciaAseo = c.Int(nullable: false),
                        FrecuenciaAseoEquipos = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Recoleccions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ControlRecoleccion = c.Boolean(nullable: false),
                        Verdes = c.Int(nullable: false),
                        Pintones = c.Int(nullable: false),
                        Maduros = c.Int(nullable: false),
                        TiempoTranscurrido = c.Int(nullable: false),
                        ManejaTolvaSifon = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Secadoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SecaTanProntoLava = c.Boolean(nullable: false),
                        TiempoSecado = c.Int(nullable: false),
                        SistemaSecado = c.String(),
                        TipoSecadoSolar = c.String(),
                        TipoSecadoMecanico = c.String(),
                        PorcentajeSecadoSolar = c.Int(nullable: false),
                        PorcentajeSecadoMecanico = c.Int(nullable: false),
                        DeterminaHumedad = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Checklists", t => t.Id)
                .Index(t => t.Id);
            
            DropColumn("dbo.criteria", "RequirementId");
            DropColumn("dbo.Images", "Principal");
            DropTable("dbo.Requirements");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Requirements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Images", "Principal", c => c.Boolean(nullable: false));
            AddColumn("dbo.criteria", "RequirementId", c => c.Guid());
            DropForeignKey("dbo.Checklists", "FarmId", "dbo.farms");
            DropForeignKey("dbo.Checklists", "UserId", "dbo.Users");
            DropForeignKey("dbo.Secadoes", "Id", "dbo.Checklists");
            DropForeignKey("dbo.Recoleccions", "Id", "dbo.Checklists");
            DropForeignKey("dbo.Mantenimientoes", "Id", "dbo.Checklists");
            DropForeignKey("dbo.Infraestructuras", "Id", "dbo.Checklists");
            DropForeignKey("dbo.Fermentacions", "Id", "dbo.Checklists");
            DropForeignKey("dbo.Despulpadoes", "Id", "dbo.Checklists");
            DropForeignKey("dbo.Almacenamientoes", "Id", "dbo.Checklists");
            DropIndex("dbo.Secadoes", new[] { "Id" });
            DropIndex("dbo.Recoleccions", new[] { "Id" });
            DropIndex("dbo.Mantenimientoes", new[] { "Id" });
            DropIndex("dbo.Infraestructuras", new[] { "Id" });
            DropIndex("dbo.Fermentacions", new[] { "Id" });
            DropIndex("dbo.Despulpadoes", new[] { "Id" });
            DropIndex("dbo.Almacenamientoes", new[] { "Id" });
            DropIndex("dbo.Checklists", new[] { "FarmId" });
            DropIndex("dbo.Checklists", new[] { "UserId" });
            DropTable("dbo.Secadoes");
            DropTable("dbo.Recoleccions");
            DropTable("dbo.Mantenimientoes");
            DropTable("dbo.Infraestructuras");
            DropTable("dbo.Fermentacions");
            DropTable("dbo.Despulpadoes");
            DropTable("dbo.Almacenamientoes");
            DropTable("dbo.Checklists");
            CreateIndex("dbo.criteria", "RequirementId");
            AddForeignKey("dbo.criteria", "RequirementId", "dbo.Requirements", "Id");
        }
    }
}
