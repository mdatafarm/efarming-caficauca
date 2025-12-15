using EFarming.Common;
using EFarming.DTO.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.ImpactModule
{
    /// <summary>
    /// CategoryDTO EntityDTO
    /// </summary>
    public class CategoryDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the assessment template identifier.
        /// </summary>
        /// <value>
        /// The assessment template identifier.
        /// </value>
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public AssessmentTemplateDTO AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the indicators.
        /// </summary>
        /// <value>
        /// The indicators.
        /// </value>
        public virtual ICollection<IndicatorDTO> Indicators { get; set; }
    }
}
