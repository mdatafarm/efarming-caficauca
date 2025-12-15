using EFarming.Common;
using System;

namespace EFarming.DTO.QualityModule
{
    /// <summary>
    /// OpenTextAttributeDTO EntityDTO
    /// </summary>
    public class OpenTextAttributeDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OpenTextAttributeDTO"/> is number.
        /// </summary>
        /// <value>
        ///   <c>true</c> if number; otherwise, <c>false</c>.
        /// </value>
        public bool Number { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute identifier.
        /// </summary>
        /// <value>
        /// The quality attribute identifier.
        /// </value>
        public Guid QualityAttributeId { get; set; }

        /// <summary>
        /// Gets or sets the quality attribute.
        /// </summary>
        /// <value>
        /// The quality attribute.
        /// </value>
        public QualityAttributeDTO QualityAttribute { get; set; }
    }
}
