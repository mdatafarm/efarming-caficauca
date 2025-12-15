using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Common
{
    /// <summary>
    /// Historical Entiry
    /// </summary>
    public class Historical : Entity
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the formated date.
        /// </summary>
        /// <value>
        /// The formated date.
        /// </value>
        public string FormatedDate
        {
            get
            {
                return Date.ToShortDateString();
            }
        }
    }
}
