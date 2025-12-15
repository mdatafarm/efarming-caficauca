using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Image Entity
    /// </summary>
    public class Image : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the thumb.
        /// </summary>
        /// <value>
        /// The name of the thumb.
        /// </value>
        [Required]
        public string ThumbName { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [Required]
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the thumb.
        /// </summary>
        /// <value>
        /// The thumb.
        /// </value>
        [Required]
        public string Thumb { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Image"/> is principal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if principal; otherwise, <c>false</c>.
        /// </value>
        public bool Principal { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        [Required]
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public Farm Farm { get; set; }
    }
}
