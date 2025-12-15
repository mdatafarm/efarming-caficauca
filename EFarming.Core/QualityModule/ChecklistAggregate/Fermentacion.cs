using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Fermentacion : Entity
    {
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public string TipoBeneficiadero { get; set; }

        public int TiempoFermentacion { get; set; }

        public string SistemaFermentacion { get; set; }

        public bool SistemaFermentacionAbierto { get; set; }

        public string CriterioIdentificacionLavado { get; set; }

        public string MaterialTanque { get; set; }

        public bool LavaDespuesFermentado { get; set; }

        public bool JuntaEnCochadas { get; set; }

        public int CuantasCochadas { get; set; }

        public int CuantosDiasCochadas { get; set; }

        public string FuenteAgua { get; set; }

        public string Observaciones { get; set; }

        public virtual Checklist Checklist { get; set; }
    }
}
