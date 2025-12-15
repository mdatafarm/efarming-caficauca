using EFarming.Common;
using System;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// FarmSubstatusDTO EntityDTO
    /// </summary>
    public class FarmSubstatusDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the farm status identifier.
        /// </summary>
        /// <value>
        /// The farm status identifier.
        /// </value>
        public Guid FarmStatusId { get; set; }

        /// <summary>
        /// Gets or sets the farm status.
        /// </summary>
        /// <value>
        /// The farm status.
        /// </value>
        public FarmStatusDTO FarmStatus { get; set; }
    }
}
