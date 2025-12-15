using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.PlantationStatusAggregate
{
    /// <summary>
    /// PlantationStatus Entity
    /// </summary>
    public class PlantationStatus : Entity
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
    }
}
