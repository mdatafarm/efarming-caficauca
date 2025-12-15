using EFarming.Common;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.PlantationVarietyAggregate
{
    /// <summary>
    /// Plantation Variety Entity
    /// </summary>
    public class PlantationVariety : Entity
    {
        /// <summary>
        /// The _name
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [MaxLength(32)]
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the plantation type identifier.
        /// </summary>
        /// <value>
        /// The plantation type identifier.
        /// </value>
        [Required]
        public Guid PlantationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the plantation.
        /// </summary>
        /// <value>
        /// The type of the plantation.
        /// </value>
        public virtual PlantationType PlantationType { get; set; }
    }
}
