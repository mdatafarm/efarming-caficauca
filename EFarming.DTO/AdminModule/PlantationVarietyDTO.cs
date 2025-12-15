using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// PlantationVarietyDTO EntityDTO
    /// </summary>
    public class PlantationVarietyDTO : EntityDTO
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

        /// <summary>
        /// Gets or sets the type of the plantation.
        /// </summary>
        /// <value>
        /// The type of the plantation.
        /// </value>
        public PlantationTypeDTO PlantationType { get; set; }
    }
}
