using EFarming.Common;
using System;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// VillageDTO EntityDTO
    /// </summary>
    public class VillageDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the municipality identifier.
        /// </summary>
        /// <value>
        /// The municipality identifier.
        /// </value>
        public Guid MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the municipality.
        /// </summary>
        /// <value>
        /// The municipality.
        /// </value>
        public MunicipalityDTO Municipality { get; set; }
    }
}
