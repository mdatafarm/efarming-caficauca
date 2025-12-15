using EFarming.Common;
using EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// FloweringPeriod Entity
    /// </summary>
    public class FloweringPeriod : Entity
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the plantation identifier.
        /// </summary>
        /// <value>
        /// The plantation identifier.
        /// </value>
        [Required]
        public Guid PlantationId { get; set; }

        [Required]
        public Guid FloweringPeriodQualificationId { get; set; }

        /// <summary>
        /// Gets or sets the plantation.
        /// </summary>
        /// <value>
        /// The plantation.
        /// </value>
        public virtual Plantation Plantation { get; set; }

        public virtual FloweringPeriodQualification FloweringPeriodQualification { get; set; }
    }
}
