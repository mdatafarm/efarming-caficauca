using EFarming.Common;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using System.Collections.Generic;

namespace EFarming.Core.AdminModule.PlantationTypeAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class PlantationType : Entity
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
        /// Gets or sets the plantation varieties.
        /// </summary>
        /// <value>
        /// The plantation varieties.
        /// </value>
        public virtual ICollection<PlantationVariety> PlantationVarieties { get; set; }

        /// <summary>
        /// Gets or sets the plantations.
        /// </summary>
        /// <value>
        /// The plantations.
        /// </value>
        public virtual ICollection<Plantation> Plantations { get; set; }
    }
}
