using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    /// <summary>
    /// Client Entity
    /// </summary>
    public partial class Client : Entity
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
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [Required]
        public int ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Required]
        public string Country { get; set; }

        //[ForeignKey("Agent")]
        //public Guid AgentId { get; set; }

        /// <summary>
        /// Gets or sets the agents.
        /// </summary>
        /// <value>
        /// The agents.
        /// </value>
        public virtual ICollection<Agent> Agents { get; set; }
        /// <summary>
        /// Gets or sets the agreements.
        /// </summary>
        /// <value>
        /// The agreements.
        /// </value>
        public virtual ICollection<Agreement> Agreements { get; set; }
    }
}
