using EFarming.Common;
using System;

namespace EFarming.DTO.APIModule
{
    /// <summary>
    /// VillageAPIDTO EntityDTO
    /// </summary>
    public class VillageAPIDTO : EntityDTO
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
    }
}
