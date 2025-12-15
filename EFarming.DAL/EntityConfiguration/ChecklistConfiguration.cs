using EFarming.Core.QualityModule.ChecklistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class ChecklistConfiguration : BasicConfiguration<Checklist>
    {
        public ChecklistConfiguration()
        {
            Property(c => c.Observaciones).IsOptional();

            HasRequired(c => c.Almacenamiento)
                .WithRequiredPrincipal(a => a.Checklist);
            HasRequired(c => c.Despulpado)
                .WithRequiredPrincipal(d => d.Checklist);
            HasRequired(c => c.Fermentacion)
                .WithRequiredPrincipal(d => d.Checklist);
            HasRequired(c => c.Infraestructura)
                .WithRequiredPrincipal(d => d.Checklist);
            HasRequired(c => c.Mantenimiento)
                .WithRequiredPrincipal(d => d.Checklist);
            HasRequired(c => c.Recoleccion)
                .WithRequiredPrincipal(d => d.Checklist);
            HasRequired(c => c.Secado)
                .WithRequiredPrincipal(d => d.Checklist);
            
            ToTable("checklists");
        }
    }
}
