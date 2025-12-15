using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.DTO.QualityModule
{
    public class SecadoDTO : EntityDTO
    {
        public bool SecaTanProntoLava { get; set; }

        public int TiempoSecado { get; set; }

        public string SistemaSecado { get; set; }

        public string TipoSecadoSolar { get; set; }

        public string TipoSecadoMecanico { get; set; }

        public int PorcentajeSecadoSolar { get; set; }

        public int PorcentajeSecadoMecanico { get; set; }

        public string DeterminaHumedad { get; set; }

        public string Observaciones { get; set; }

        public ChecklistDTO Checklist { get; set; }
    }
}
