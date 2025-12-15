using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.QualityModule.DashboardAggregate
{
    /// <summary>
    /// Clasification Entity
    /// </summary>
    public class Clasification
    {
        /// <summary>
        /// Gets or sets the cooperative.
        /// </summary>
        /// <value>
        /// The cooperative.
        /// </value>
        public string Cooperative { get; set; }

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
    }
}
