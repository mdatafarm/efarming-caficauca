using EFarming.Common;
using EFarming.Core.AdminModule.FarmSubstatusAggregate;
using System.Collections.Generic;

namespace EFarming.Core.AdminModule.FarmStatusAggregate
{
    /// <summary>
    /// FarmStatus Entity
    /// </summary>
    public class FarmStatus : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmStatus"/> class.
        /// </summary>
        public FarmStatus()
        {
            FarmSubstatuses = new List<FarmSubstatus>();
        }

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
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the farm substatuses.
        /// </summary>
        /// <value>
        /// The farm substatuses.
        /// </value>
        public virtual ICollection<FarmSubstatus> FarmSubstatuses { get; set; }
    }
}
