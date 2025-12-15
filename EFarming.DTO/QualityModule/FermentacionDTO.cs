using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFarming.DTO.QualityModule
{
    public class FermentacionDTO : EntityDTO
    {
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

        public ChecklistDTO Checklist { get; set; }
    }
}
