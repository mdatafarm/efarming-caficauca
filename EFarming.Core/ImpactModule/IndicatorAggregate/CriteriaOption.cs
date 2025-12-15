using EFarming.Common;
using EFarming.Core.ImpactModule.ImpactAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// CriteriaOption entity
    /// </summary>
    public class CriteriaOption : Entity
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the criteria identifier.
        /// </summary>
        /// <value>
        /// The criteria identifier.
        /// </value>
        [Required]
        public Guid CriteriaId { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public virtual Criteria Criteria { get; set; }

        /// <summary>
        /// Gets or sets the impact assessments.
        /// </summary>
        /// <value>
        /// The impact assessments.
        /// </value>
        public virtual ICollection<ImpactAssessment> ImpactAssessments { get; set; }
    }
}
