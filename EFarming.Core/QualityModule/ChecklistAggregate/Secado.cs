using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Secado : Entity
    {
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public bool SecaTanProntoLava { get; set; }

        public int TiempoSecado { get; set; }

        public string SistemaSecado { get; set; }

        public string TipoSecadoSolar { get; set; }

        public string TipoSecadoMecanico { get; set; }

        public int PorcentajeSecadoSolar { get; set; }

        public int PorcentajeSecadoMecanico { get; set; }

        public string DeterminaHumedad { get; set; }

        public string Observaciones { get; set; }

        public virtual Checklist Checklist { get; set; }
    }
}
