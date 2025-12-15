using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    /// <summary>
    /// More Information Entity
    /// </summary>
    public class MoreInformation : Entity
    {
        /// <summary>
        /// Gets or sets the type of the information.
        /// </summary>
        /// <value>
        /// The type of the information.
        /// </value>
        [Required]
        public string InformationType { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the short.
        /// </summary>
        /// <value>
        /// The short.
        /// </value>
        [MaxLength(50)]
        public string Short { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// The reference that appears in the invoice.
        /// </summary>
        /// <value>
        /// The invoice reference.
        /// </value>
        [MaxLength(250)]
        public string InvoiceReference { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public virtual Client Client { get; set; }
    }
}
