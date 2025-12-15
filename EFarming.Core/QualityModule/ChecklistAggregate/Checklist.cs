using EFarming.Common;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using System;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Checklist : Historical
    {
        public string Observaciones { get; set; }

        public Guid UserId { get; set; }

        public Guid FarmId { get; set; }

        public string TechnicianSignatureUrl { get; set; }

        public string FarmerSignatureUrl { get; set; }

        public virtual Farm Farm { get; set; }

        public virtual User User{ get; set; }

        public virtual Almacenamiento Almacenamiento { get; set; }

        public virtual Despulpado Despulpado { get; set; }

        public virtual Fermentacion Fermentacion { get; set; }

        public virtual Infraestructura Infraestructura { get; set; }

        public virtual Mantenimiento Mantenimiento { get; set; }

        public virtual Recoleccion Recoleccion { get; set; }

        public virtual Secado Secado { get; set; }
    }
}
