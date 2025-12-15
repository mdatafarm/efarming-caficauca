using EFarming.Common;
using EFarming.DTO.FarmModule;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EFarming.DTO.ProjectModule
{
    /// <summary>
    /// ProjectDTO EntityDTO
    /// </summary>
    public class ProjectDTO : EntityDTO
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

        //control the serialization behavior property
        //[JsonIgnore]
        //public virtual List<FarmDTO> Farms { get; set; }
    }
}
