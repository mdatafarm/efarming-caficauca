using EFarming.Common;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using System.Collections.Generic;

namespace EFarming.Core.ProjectModule.ProjectAggregate
{
    /// <summary>
    /// Project Entity
    /// </summary>
    public class Project : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public virtual ICollection<Farm> Farms { get; set; }

        /// <summary>
        /// Gets or sets the associated people.
        /// </summary>
        /// <value>
        /// The associated people.
        /// </value>
        public virtual ICollection<User> Users { get; set; }
    }
}   
