using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// Criteria Entity
    /// </summary>
    public class Criteria : Entity
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
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
        /// Gets or sets the indicator identifier.
        /// </summary>
        /// <value>
        /// The indicator identifier.
        /// </value>
        [Required]
        public Guid IndicatorId { get; set; }

        /// <summary>
        /// Gets or sets the requirement identifier.
        /// </summary>
        /// <value>
        /// The requirement identifier.
        /// </value>
        public Guid? RequirementId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Criteria"/> is mandatory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mandatory; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [DefaultValue(false)]
        public bool Mandatory { get; set; }

        /// <summary>
        /// Gets or sets the requirement.
        /// </summary>
        /// <value>
        /// The requirement.
        /// </value>
        public virtual Requirement Requirement { get; set; }

        /// <summary>
        /// Gets or sets the indicator.
        /// </summary>
        /// <value>
        /// The indicator.
        /// </value>
        public virtual Indicator Indicator { get; set; }

        /// <summary>
        /// Gets or sets the criteria options.
        /// </summary>
        /// <value>
        /// The criteria options.
        /// </value>
        public virtual ICollection<CriteriaOption> CriteriaOptions { get; set; }
    }
}
