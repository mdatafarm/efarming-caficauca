using EFarming.Common;
using System;
using System.Collections.Generic;

namespace EFarming.DTO.APIModule
{
    /// <summary>
    /// MunicipalityAPIDTO EntityDTO
    /// </summary>
    public class MunicipalityAPIDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the villages.
        /// </summary>
        /// <value>
        /// The villages.
        /// </value>
        public ICollection<VillageAPIDTO> Villages { get; set; }
    }
}
