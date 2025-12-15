using EFarming.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Fertilizer
    /// </summary>
    public class Fertilizer : Historical
    {
        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the farmer identification.
        /// </summary>
        /// <value>
        /// The farmer identification.
        /// </value>
        public string Identification { get; set; }
        /// <summary>
        /// Gets or sets the ubication.
        /// </summary>
        /// <value>
        /// The ubication.
        /// </value>
        public int Ubication { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the hold.
        /// </summary>
        /// <value>
        /// The hold.
        /// </value>
        public int Hold { get; set; }

        /// <summary>
        /// Gets or sets the cash register.
        /// </summary>
        /// <value>
        /// The cash register.
        /// </value>
        public int CashRegister { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        public int UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

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
        public virtual Farm Farm { get; set; }
    }
}
