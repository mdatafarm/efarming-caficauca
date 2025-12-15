using EFarming.Common;
using EFarming.Core.AdminModule.PlantationStatusAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Plantation entity
    /// </summary>
    public class Plantation : Entity
    {
        /// <summary>
        /// Gets or sets the hectares.
        /// </summary>
        /// <value>
        /// The hectares.
        /// </value>
        [MaxLength(20)]
        public string Hectares { get; set; }

        /// <summary>
        /// Gets or sets the trees distance.
        /// </summary>
        /// <value>
        /// The trees distance.
        /// </value>
        [MaxLength(20)]
        public string TreesDistance { get; set; }

        /// <summary>
        /// Gets or sets the groove distance.
        /// </summary>
        /// <value>
        /// The groove distance.
        /// </value>
        [MaxLength(20)]
        public string GrooveDistance { get; set; }

        /// <summary>
        /// Has a calculated value depending of the distances value
        /// </summary>
        /// <value>
        /// The density.
        /// </value>
        [MaxLength(20)]
        public string Density { get; set; }

        /// <summary>
        /// Gets or sets the estimated production.
        /// </summary>
        /// <value>
        /// The estimated production.
        /// </value>
        [MaxLength(20)]
        public string EstimatedProduction { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public DateTime Age { get; set; }

        /// <summary>
        /// Gets or sets the number of plants.
        /// </summary>
        /// <value>
        /// The number of plants.
        /// </value>
        [Required]
        public int NumberOfPlants { get; set; }

        /// <summary>
        /// Gets or sets the plantation status identifier.
        /// </summary>
        /// <value>
        /// The plantation status identifier.
        /// </value>
        [Required]
        public Guid PlantationStatusId { get; set; }

        /// <summary>
        /// Gets or sets the productivity identifier.
        /// </summary>
        /// <value>
        /// The productivity identifier.
        /// </value>
        [Required]
        public Guid ProductivityId { get; set; }

        /// <summary>
        /// Gets or sets the plantation type identifier.
        /// </summary>
        /// <value>
        /// The plantation type identifier.
        /// </value>
        [Required]
        public Guid PlantationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the plantation variety identifier.
        /// </summary>
        /// <value>
        /// The plantation variety identifier.
        /// </value>
        [Required]
        public Guid PlantationVarietyId { get; set; }

        /// <summary>
        /// Gets or sets the plantation status.
        /// </summary>
        /// <value>
        /// The plantation status.
        /// </value>
        public virtual PlantationStatus PlantationStatus { get; set; }

        /// <summary>
        /// Gets or sets the productivity.
        /// </summary>
        /// <value>
        /// The productivity.
        /// </value>
        public virtual Productivity Productivity { get; set; }

        /// <summary>
        /// Gets or sets the type of the plantation.
        /// </summary>
        /// <value>
        /// The type of the plantation.
        /// </value>
        public virtual PlantationType PlantationType { get; set; }

        /// <summary>
        /// Gets or sets the flowering periods.
        /// </summary>
        /// <value>
        /// The flowering periods.
        /// </value>
        public virtual ICollection<FloweringPeriod> FloweringPeriods { get; set; }

        /// <summary>
        /// Gets or sets the plantation variety.
        /// </summary>
        /// <value>
        /// The plantation variety.
        /// </value>
        public virtual PlantationVariety PlantationVariety { get; set; }

        [Required(ErrorMessage = "El numero de lote debe ser especificado.")]
        public int NumberLot { get; set; }

        [Required(ErrorMessage = "El municipio del lote debe ser especificado.")]
        public Guid MuniLot { get; set; }

        [Required(ErrorMessage = "La vereda del lote debe ser especificado.")]
        public Guid VillageLot { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "El nombre del lote debe ser especificado.")]
        public string NomLot { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "La labor del lote debe ser especificada.")]
        public string LabLot { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "El tipo del lote debe ser especificado.")]
        public string TipoLot { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "La forma de siembra del lote debe ser especificada.")]
        public string FormLot { get; set; }

        [Required(ErrorMessage = "El numero de ejers por arbol del lote debe ser especificado.")]
        public int NumEjeArbLot { get; set; }

        [MaxLength(20)]
        public string EstimatedProductionManual { get; set; }
    }
}
