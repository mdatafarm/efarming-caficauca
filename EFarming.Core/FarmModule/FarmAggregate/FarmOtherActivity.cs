using EFarming.Common;
using EFarming.Core.AdminModule.OtherActivitiesAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// FarmOtherActivity Entity
    /// </summary>
    public class FarmOtherActivity : Entity
    {
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        [Required]
        public double Percentage { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        [Required]
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the other activity identifier.
        /// </summary>
        /// <value>
        /// The other activity identifier.
        /// </value>
        [Required]
        public Guid OtherActivityId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }

        /// <summary>
        /// Gets or sets the other activity.
        /// </summary>
        /// <value>
        /// The other activity.
        /// </value>
        public virtual OtherActivity OtherActivity { get; set; }
    }
}
