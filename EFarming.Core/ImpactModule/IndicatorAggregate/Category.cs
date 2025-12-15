using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// Category entity
    /// </summary>
    public class Category : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        [Required]
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the assessment template identifier.
        /// </summary>
        /// <value>
        /// The assessment template identifier.
        /// </value>
        [Required]
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the indicators.
        /// </summary>
        /// <value>
        /// The indicators.
        /// </value>
        public virtual ICollection<Indicator> Indicators { get; set; }
    }
}
