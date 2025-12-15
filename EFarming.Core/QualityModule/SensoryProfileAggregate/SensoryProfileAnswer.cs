using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// SensoryPrifileAnswer entity
    /// </summary>
    public class SensoryProfileAnswer : Entity
    {
        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessment identifier.
        /// </summary>
        /// <value>
        /// The sensory profile assessment identifier.
        /// </value>
        [Required]
        public Guid SensoryProfileAssessmentId { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute identifier.
        /// </summary>
        /// <value>
        /// The quality attribute identifier.
        /// </value>
        [Required]
        public Guid QualityAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessment.
        /// </summary>
        /// <value>
        /// The sensory profile assessment.
        /// </value>
        public virtual SensoryProfileAssessment SensoryProfileAssessment { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttribute QualityAttribute { get; set; }
    }
}
