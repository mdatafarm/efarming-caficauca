using EFarming.Common;
using System;

namespace EFarming.DTO.ImpactModule
{
    /// <summary>
    /// CriteriaOptionDTO EntityDTO
    /// </summary>
    public class CriteriaOptionDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the criteria identifier.
        /// </summary>
        /// <value>
        /// The criteria identifier.
        /// </value>
        public Guid CriteriaId { get; set; }

        /// <summary>
        /// Gets or sets the indicator identifier.
        /// </summary>
        /// <value>
        /// The indicator identifier.
        /// </value>
        public Guid IndicatorId { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public CriteriaDTO Criteria { get; set; }
    }
}
