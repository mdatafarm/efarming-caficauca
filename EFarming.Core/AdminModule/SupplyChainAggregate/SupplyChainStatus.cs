using EFarming.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.SupplyChainAggregate
{
    /// <summary>
    /// SupplyChainStatus Entity
    /// </summary>
    public class SupplyChainStatus : Entity
    {
        /// <summary>
        /// The _name
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the supply chains.
        /// </summary>
        /// <value>
        /// The supply chains.
        /// </value>
        public virtual ICollection<SupplyChain> SupplyChains { get; set; }
    }
}
