using EFarming.Common;
using EFarming.DTO.ImpactModule;
using EFarming.DTO.QualityModule;
using System.Collections.Generic;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// AssessmentTemplate EntityDTO
    /// </summary>
    public class AssessmentTemplateDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the impact assessments.
        /// </summary>
        /// <value>
        /// The impact assessments.
        /// </value>
        public ICollection<ImpactAssessmentDTO> ImpactAssessments { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public ICollection<SensoryProfileAssessmentDTO> SensoryProfileAssessments { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public ICollection<CategoryDTO> Categories { get; set; }

        /// <summary>
        /// Gets or sets the quality attributes.
        /// </summary>
        /// <value>
        /// The quality attributes.
        /// </value>
        public ICollection<QualityAttributeDTO> QualityAttributes { get; set; }
    }
}
