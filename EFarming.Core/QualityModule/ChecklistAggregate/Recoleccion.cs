using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Recoleccion : Entity
    {
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }
        public bool ControlRecoleccion { get; set; }

        public int Verdes { get; set; }

        public int Pintones { get; set; }

        public int Maduros { get; set; }

        public int TiempoTranscurrido { get; set; }

        public bool ManejaTolvaSifon { get; set; }

        public string Observaciones { get; set; }

        public virtual Checklist Checklist { get; set; }
    }
}
