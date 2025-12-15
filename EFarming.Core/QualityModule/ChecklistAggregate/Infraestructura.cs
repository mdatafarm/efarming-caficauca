using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Infraestructura : Entity
    {
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public string Tolva { get; set; }

        public string Despulpadora { get; set; }

        public string Desmucilaginador { get; set; }

        public string TanqueFermentacion { get; set; }

        public string CanalesCarreteo { get; set; }

        public string PisoBeneficiadero { get; set; }

        public string SistemaSecadoMecanico { get; set; }

        public string SistemaSecadoSolar { get; set; }

        public string BodegaAlmacenamiento { get; set; }

        public string Observaciones { get; set; }

        public virtual Checklist Checklist { get; set; }
    }
}
