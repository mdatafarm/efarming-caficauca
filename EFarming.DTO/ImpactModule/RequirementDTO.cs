using EFarming.Common;
using System.Collections.Generic;

namespace EFarming.DTO.ImpactModule
{
    /// <summary>
    /// RequirementDTO EntityDTO
    /// </summary>
    public class RequirementDTO : EntityDTO
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
        public List<CriteriaDTO> Criteria { get; set; }
    }
}
