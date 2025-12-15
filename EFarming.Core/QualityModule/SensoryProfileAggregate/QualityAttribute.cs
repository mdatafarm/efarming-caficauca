using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// QualityAttribute Entity
    /// </summary>
    public class QualityAttribute : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QualityAttribute"/> class.
        /// </summary>
        public QualityAttribute()
        {
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [MaxLength(128)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of.
        /// </summary>
        /// <value>
        /// The type of.
        /// </value>
        [Required]
        [MaxLength(16)]
        public string TypeOf { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [Required]
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the assessment template identifier.
        /// </summary>
        /// <value>
        /// The assessment template identifier.
        /// </value>
        [Required]
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the open text attribute.
        /// </summary>
        /// <value>
        /// The open text attribute.
        /// </value>
        public virtual OpenTextAttribute OpenTextAttribute { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the range attribute.
        /// </summary>
        /// <value>
        /// The range attribute.
        /// </value>
        public virtual RangeAttribute RangeAttribute { get; set; }

        /// <summary>
        /// Gets or sets the option attributes.
        /// </summary>
        /// <value>
        /// The option attributes.
        /// </value>
        public virtual List<OptionAttribute> OptionAttributes { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile answers.
        /// </summary>
        /// <value>
        /// The sensory profile answers.
        /// </value>
        public virtual ICollection<SensoryProfileAnswer> SensoryProfileAnswers { get; set; }
    }
}
