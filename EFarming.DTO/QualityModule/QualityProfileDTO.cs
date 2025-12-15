using EFarming.Common;
using EFarming.DTO.AdminModule;
using System.Collections.Generic;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// QualityProfileDTO EntityDTO
    /// </summary>
    public class QualityProfileDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the supply chains.
        /// </summary>
        /// <value>
        /// The supply chains.
        /// </value>
        public ICollection<SupplyChainDTO> SupplyChains { get; set; }
    }
}
