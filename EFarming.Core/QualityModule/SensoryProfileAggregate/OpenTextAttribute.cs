using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// OpenTextAttribute Entity
    /// </summary>
    public class OpenTextAttribute : Entity
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OpenTextAttribute"/> is number.
        /// </summary>
        /// <value>
        ///   <c>true</c> if number; otherwise, <c>false</c>.
        /// </value>
        public bool Number { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public virtual QualityAttribute QualityAttribute { get; set; }
    }
}
