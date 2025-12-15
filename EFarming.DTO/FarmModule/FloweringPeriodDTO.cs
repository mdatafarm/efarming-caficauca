using EFarming.Common;
using EFarming.DTO.AdminModule;
using System;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// FloweringPeriodDTO EntityDTO
    /// </summary>
    public class FloweringPeriodDTO : EntityDTO
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
        public Guid PlantationId { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the plantation.
        /// </summary>
        /// <value>
        /// The plantation.
        /// </value>
        public virtual PlantationDTO Plantation { get; set; }

        public Guid FloweringPeriodQualificationId { get; set; }

        public virtual FloweringPeriodQualificationDTO FloweringPeriodQualification { get; set; }

        /// <summary>
        /// Gets the start date formated.
        /// </summary>
        /// <value>
        /// The start date formated.
        /// </value>
        public string StartDateFormated
        {
            get
            {
                return string.Format("{0:yyyy-MM-dd}", StartDate);
            }
        }
    }
}
