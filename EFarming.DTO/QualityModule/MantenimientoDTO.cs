using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.DTO.QualityModule
{
    public class MantenimientoDTO : EntityDTO
    {
        public string OrganizacionLimpiezaBeneficiadero { get; set; }

        public bool PuedeContaminarse { get; set; }

        public string OrganizacionLimpiezaEquiposBeneficiadero { get; set; }

        public int FrecuenciaAseo { get; set; }

        public int FrecuenciaAseoEquipos { get; set; }

        public string Observaciones { get; set; }

        public ChecklistDTO Checklist { get; set; }
    }
}
