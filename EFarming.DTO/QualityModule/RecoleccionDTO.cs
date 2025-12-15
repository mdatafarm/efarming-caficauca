using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.DTO.QualityModule
{
    public class RecoleccionDTO : EntityDTO
    {
        public bool ControlRecoleccion { get; set; }

        public int Verdes { get; set; }

        public int Pintones { get; set; }

        public int Maduros { get; set; }

        public int TiempoTranscurrido { get; set; }

        public bool ManejaTolvaSifon { get; set; }

        public string Observaciones { get; set; }

        public ChecklistDTO Checklist { get; set; }
    }
}
