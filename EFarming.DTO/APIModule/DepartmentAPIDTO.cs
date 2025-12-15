using EFarming.Common;
using System.Collections.Generic;

namespace EFarming.DTO.APIModule
{
    /// <summary>
    /// DepartmentAPIDTO EntityDTO
    /// </summary>
    public class DepartmentAPIDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the municipalities.
        /// </summary>
        /// <value>
        /// The municipalities.
        /// </value>
        public ICollection<MunicipalityAPIDTO> Municipalities { get; set; }
    }
}
