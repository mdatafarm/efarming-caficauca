using EFarming.Common;
using EFarming.Core.AdminModule.SupplierAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.CountryAggregate
{
    /// <summary>
    /// Contry Entity
    /// </summary>
    public class Country : Entity
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
        [MaxLength(64)]
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public int code { get; set; }

        /// <summary>
        /// Gets or sets the suppliers.
        /// </summary>
        /// <value>
        /// The suppliers.
        /// </value>
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
