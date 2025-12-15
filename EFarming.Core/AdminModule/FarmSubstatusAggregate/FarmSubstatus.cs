using EFarming.Common;
using EFarming.Core.AdminModule.FarmStatusAggregate;
using System;

namespace EFarming.Core.AdminModule.FarmSubstatusAggregate
{
    /// <summary>
    /// FarmSubstatus Entity
    /// </summary>
    public class FarmSubstatus : Entity
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
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the farm status identifier.
        /// </summary>
        /// <value>
        /// The farm status identifier.
        /// </value>
        public Guid FarmStatusId { get; set; }

        /// <summary>
        /// Gets or sets the farm status.
        /// </summary>
        /// <value>
        /// The farm status.
        /// </value>
        public virtual FarmStatus FarmStatus { get; set; }
    }
}
