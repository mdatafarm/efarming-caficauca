using EFarming.Common;
using System;
using System.Collections.Generic;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// FarmStatusDTO EntityDTO
    /// </summary>
    public class FarmStatusDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the farm substatuses.
        /// </summary>
        /// <value>
        /// The farm substatuses.
        /// </value>
        public ICollection<FarmSubstatusDTO> FarmSubstatuses { get; set; }
    }
}
