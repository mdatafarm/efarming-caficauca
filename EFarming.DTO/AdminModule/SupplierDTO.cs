using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// SupplierDTO EntityDTO
    /// </summary>
    public class SupplierDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the logo URL.
        /// </summary>
        /// <value>
        /// The logo URL.
        /// </value>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public Guid CountryId { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public CountryDTO Country { get; set; }

        /// <summary>
        /// Gets or sets the supply chains.
        /// </summary>
        /// <value>
        /// The supply chains.
        /// </value>
        public List<SupplyChainDTO> SupplyChains { get; set; }
    }
}
