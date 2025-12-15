namespace EFarming.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddObservationToChecklistEntities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Almacenamientoes", "Observaciones", c => c.String());
            AddColumn("dbo.Despulpadoes", "Observaciones", c => c.String());
            AddColumn("dbo.Fermentacions", "Observaciones", c => c.String());
            AddColumn("dbo.Infraestructuras", "Observaciones", c => c.String());
            AddColumn("dbo.Mantenimientoes", "Observaciones", c => c.String());
            AddColumn("dbo.Recoleccions", "Observaciones", c => c.String());
            AddColumn("dbo.Secadoes", "Observaciones", c => c.String());
            DropColumn("dbo.Checklists", "AlmacenamientoId");
            DropColumn("dbo.Checklists", "DespulpadoId");
            DropColumn("dbo.Checklists", "FermentacionId");
            DropColumn("dbo.Checklists", "InfraestucturaId");
            DropColumn("dbo.Checklists", "MantenimientoId");
            DropColumn("dbo.Checklists", "RecoleccionId");
            DropColumn("dbo.Checklists", "SecadoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Checklists", "SecadoId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checklists", "RecoleccionId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checklists", "MantenimientoId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checklists", "InfraestucturaId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checklists", "FermentacionId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checklists", "DespulpadoId", c => c.Guid(nullable: false));
            AddColumn("dbo.Checklists", "AlmacenamientoId", c => c.Guid(nullable: false));
            DropColumn("dbo.Secadoes", "Observaciones");
            DropColumn("dbo.Recoleccions", "Observaciones");
            DropColumn("dbo.Mantenimientoes", "Observaciones");
            DropColumn("dbo.Infraestructuras", "Observaciones");
            DropColumn("dbo.Fermentacions", "Observaciones");
            DropColumn("dbo.Despulpadoes", "Observaciones");
            DropColumn("dbo.Almacenamientoes", "Observaciones");
        }
    }
}
