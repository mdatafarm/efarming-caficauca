using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public class Mantenimiento : Entity
    {
        [Key, ForeignKey("Checklist")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        public string OrganizacionLimpiezaBeneficiadero { get; set; }

        public bool PuedeContaminarse { get; set; }

        public string OrganizacionLimpiezaEquiposBeneficiadero { get; set; }

        public int FrecuenciaAseo { get; set; }

        public int FrecuenciaAseoEquipos { get; set; }

        public string Observaciones { get; set; }

        public virtual Checklist Checklist { get; set; }
    }
}
