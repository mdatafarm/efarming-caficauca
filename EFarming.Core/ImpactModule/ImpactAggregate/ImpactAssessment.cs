using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFarming.Core.ImpactModule.ImpactAggregate
{
    /// <summary>
    /// ImpactAssessment Entity
    /// </summary>
    public class ImpactAssessment : Entity
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
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the answers.
        /// </summary>
        /// <value>
        /// The answers.
        /// </value>
        public virtual ICollection<CriteriaOption> Answers { get; set; }

        #region Public Methods
        /// <summary>
        /// Gets the answers.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Indicator, int> GetAnswers()
        {
            return Answers.GroupBy(a => a.Criteria.Indicator, new EntityComparer<Indicator>())
                .Select(group => new { Indicator = group.Key, Sum = group.Sum(g => g.Value)})
                .ToDictionary(v => v.Indicator, v => v.Sum);
        }
        #endregion
    }
}
