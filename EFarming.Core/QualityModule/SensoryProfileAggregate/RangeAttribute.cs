using EFarming.Common;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// RangeAttribute Entity
    /// </summary>
    public class RangeAttribute : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute"/> class.
        /// </summary>
        public RangeAttribute()
        {
            MinVal = 0;
            MaxVal = 0;
            Step = 0;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        [Required]
        public double MinVal { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        [Required]
        public double MaxVal { get; set; }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        [Required]
        public double Step { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttribute QualityAttribute { get; set; }
    }
}
