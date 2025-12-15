using EFarming.Common;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.OwnershipTypeAggregate
{
    /// <summary>
    /// Ownershiptype Entity
    /// </summary>
    public class OwnershipType : Entity
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
