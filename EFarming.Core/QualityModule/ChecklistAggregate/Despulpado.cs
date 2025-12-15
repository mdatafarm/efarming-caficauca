using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Despulpado : Entity
    {
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public bool SistemaClasificacion { get; set; }

        public bool UtilizaAgua { get; set; }

        public bool PasaFermentacion { get; set; }

        public string Observaciones { get; set; }

        public virtual Checklist Checklist { get; set; }
    }
}
