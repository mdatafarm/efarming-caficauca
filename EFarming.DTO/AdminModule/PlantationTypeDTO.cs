using EFarming.Common;
using System.Collections.Generic;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// PlantationTypeDTO EntityDTO
    /// </summary>
    public class PlantationTypeDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the plantation varieties.
        /// </summary>
        /// <value>
        /// The plantation varieties.
        /// </value>
        public ICollection<PlantationVarietyDTO> PlantationVarieties { get; set; }
    }
}
