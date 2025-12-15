using EFarming.Common;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.QualityModule.QualityProfileAggregate
{
    /// <summary>
    /// QualityProfile Entity
    /// </summary>
    public class QualityProfile : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the supply chains.
        /// </summary>
        /// <value>
        /// The supply chains.
        /// </value>
        public virtual ICollection<SupplyChain> SupplyChains { get; set; }
    }
}
