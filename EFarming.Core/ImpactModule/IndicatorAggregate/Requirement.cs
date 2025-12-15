using EFarming.Common;
using System.Collections.Generic;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// Requirement Entity
    /// </summary>
    public class Requirement : Entity
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public virtual ICollection<Criteria> Criteria { get; set; }
    }
}
