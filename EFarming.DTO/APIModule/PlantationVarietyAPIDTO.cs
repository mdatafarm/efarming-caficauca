using EFarming.Common;
using System;

namespace EFarming.DTO.APIModule
{
    /// <summary>
    /// PlantationVarietyAPIDTO EntityDTO
    /// </summary>
    public class PlantationVarietyAPIDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the plantation type identifier.
        /// </summary>
        /// <value>
        /// The plantation type identifier.
        /// </value>
        public Guid PlantationTypeId { get; set; }
    }
}
