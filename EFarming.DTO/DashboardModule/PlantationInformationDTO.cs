using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.DashboardModule
{
    /// <summary>
    /// PlantationInformationDTO 
    /// </summary>
    public class PlantationInformationDTO
    {
        /// <summary>
        /// Gets or sets the plantation.
        /// </summary>
        /// <value>
        /// The plantation.
        /// </value>
        public string Plantation { get; set; }

        /// <summary>
        /// Gets or sets the varieties.
        /// </summary>
        /// <value>
        /// The varieties.
        /// </value>
        public IEnumerable<string> Varieties { get; set; }

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        /// <value>
        /// The area.
        /// </value>
        public double Area { get; set; }
    }
}
