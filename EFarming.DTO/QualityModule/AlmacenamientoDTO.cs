using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.DTO.QualityModule
{
    public class AlmacenamientoDTO : EntityDTO
    {
        public bool AlmacenamientoExclusivoCafe { get; set; }

        public string TipoEmpaqueCafeNespresso { get; set; }

        public bool AlmacenaProductosContaminantes { get; set; }

        public bool CondicionesMinimas { get; set; }

        public string LugarAdecuado { get; set; }

        public string TipoEstopa { get; set; }

        public string Observaciones { get; set; }

        public ChecklistDTO Checklist { get; set; }
    }
}
