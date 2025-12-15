using EFarming.Common;
using EFarming.Common.Consts;
using EFarming.Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// OptionAttribute Entity
    /// </summary>
    public class OptionAttribute : Entity
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Definition { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute identifier.
        /// </summary>
        /// <value>
        /// The quality attribute identifier.
        /// </value>
        [Required]
        public Guid QualityAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the SensoryProfileModuleId.
        /// </summary>
        /// <value>
        /// The SensoryProfileModuleId.
        /// </value>
        public int? SensoryProfileModuleId { get; set; }

        /// <summary>
        /// Gets or sets the SensoryProfileModules.
        /// </summary>
        /// <value>
        /// The SensoryProfileModules.
        /// </value>
        [ForeignKey("SensoryProfileModuleId")]
        public virtual SensoryProfileModules SensoryProfileModules { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttribute QualityAttribute { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual ICollection<QualityRecommendations> QualityRecommendations { get; set; }
    }
}
