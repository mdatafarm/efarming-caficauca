using EFarming.Common;
using System.Collections.Generic;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// SupplyChainStatusDTO EntityDTO
    /// </summary>
    public class SupplyChainStatusDTO : EntityDTO
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
