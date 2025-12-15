using EFarming.Common;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.OtherActivitiesAggregate
{
    /// <summary>
    /// OtherActivity Entity
    /// </summary>
    public class OtherActivity: Entity
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
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }
    }
}
