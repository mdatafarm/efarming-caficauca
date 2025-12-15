using EFarming.Common;
using System.Collections.Generic;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// CountryDTO EntityDTO
    /// </summary>
    public class CountryDTO:EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the suppliers.
        /// </summary>
        /// <value>
        /// The suppliers.
        /// </value>
        public List<SupplierDTO> Suppliers { get; set; }
    }
}
