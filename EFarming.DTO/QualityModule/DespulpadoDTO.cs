using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.DTO.QualityModule
{
    public class DespulpadoDTO : EntityDTO
    {
        public bool SistemaClasificacion { get; set; }

        public bool UtilizaAgua { get; set; }

        public bool PasaFermentacion { get; set; }

        public string Observaciones { get; set; }

        public ChecklistDTO Checklist { get; set; }
    }
}
