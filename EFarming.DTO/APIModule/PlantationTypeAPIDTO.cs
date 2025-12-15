using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.APIModule
{
    /// <summary>
    /// PlantationTypeAPIDTO EntityDTO
    /// </summary>
    public class PlantationTypeAPIDTO : EntityDTO
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
        public ICollection<PlantationVarietyAPIDTO> PlantationVarieties { get; set; }
    }
}
