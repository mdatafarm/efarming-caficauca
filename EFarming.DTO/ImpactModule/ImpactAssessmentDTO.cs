using EFarming.Common;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.DTO.ImpactModule
{
    /// <summary>
    /// ImpactAssessmentDTO EntityDTO
    /// </summary>
    public class ImpactAssessmentDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required]
        public DateTime Date { get; set; }

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
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        [Required]
        public Guid FarmId { get; set; }

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
        public AssessmentTemplateDTO AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        public virtual ICollection<CriteriaOptionDTO> Answers { get; set; }
    }
}
