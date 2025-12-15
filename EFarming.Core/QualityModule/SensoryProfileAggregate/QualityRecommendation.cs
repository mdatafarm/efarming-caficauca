using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// QualityRecommendations Entity
    /// </summary>
    public class QualityRecommendations : Entity
    {
        /// <summary>
        /// Gets or sets the Text.
        /// </summary>
        /// <value>
        /// The Text.
        /// </value>

        public string Recommendations { get; set; }

        /// <summary>
        /// Gets or sets the Information Type.
        /// </summary>
        /// <value>
        /// The Information Type.
        /// </value>
        public string InformationType { get; set; }

        /// <summary>
        /// Gets or sets the Order.
        /// </summary>
        /// <value>
        /// The Order.
        /// </value>
        public int? OrderOpcion { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public virtual ICollection<OptionAttribute> OptionAttribute { get; set; }
    }
}
