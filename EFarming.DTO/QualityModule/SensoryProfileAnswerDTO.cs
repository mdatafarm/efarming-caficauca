using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// SensoryProfileAnswerDTO EntityDTO
    /// </summary>
    public class SensoryProfileAnswerDTO : EntityDTO
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
        public Guid SensoryProfileAssessmentId { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute identifier.
        /// </summary>
        /// <value>
        /// The quality attribute identifier.
        /// </value>
        public Guid QualityAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessment.
        /// </summary>
        /// <value>
        /// The sensory profile assessment.
        /// </value>
        public virtual SensoryProfileAssessmentDTO SensoryProfileAssessment { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttributeDTO QualityAttribute { get; set; }
    }
}
