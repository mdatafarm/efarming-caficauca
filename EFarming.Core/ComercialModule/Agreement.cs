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
    /// Agreement Entity
    /// </summary>
    public partial class Agreement : Entity
    {
        /// <summary>
        /// Gets or sets your reference.
        /// </summary>
        /// <value>
        /// Your reference.
        /// </value>
        public string YourRef { get; set; }

        /// <summary>
        /// Gets or sets the second client reference.
        /// </summary>
        /// <value>
        /// The second client reference.
        /// </value>
        public string SecondClientRef { get; set; }

        /// <summary>
        /// Gets or sets our reference.
        /// </summary>
        /// <value>
        /// Our reference.
        /// </value>
        public string OurRef { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// Gets or sets the agent identifier.
        /// </summary>
        /// <value>
        /// The agent identifier.
        /// </value>
        [Display(Name = "Agent")]
        public Guid AgentId { get; set; }

        /// <summary>
        /// Gets or sets the seller identifier.
        /// </summary>
        /// <value>
        /// The seller identifier.
        /// </value>
        [Display(Name = "Seller")]
        public Guid SellerId { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public int Volume { get; set; }

        /// <summary>
        /// Gets or sets the shipment date.
        /// </summary>
        /// <value>
        /// The shipment date.
        /// </value>
        public DateTime ShipmentDate { get; set; }

        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        /// <value>
        /// The quality.
        /// </value>
        public string Quality { get; set; }

        /// <summary>
        /// Gets or sets the type of the price.
        /// </summary>
        /// <value>
        /// The type of the price.
        /// </value>
        public string PriceType { get; set; }

        /// <summary>
        /// Gets or sets the price lots.
        /// </summary>
        /// <value>
        /// The price lots.
        /// </value>
        public int? LotsNumber { get; set; }

        /// <summary>
        /// Gets or sets the price date.
        /// </summary>
        /// <value>
        /// The price date.
        /// </value>
        public string PriceDate { get; set; }

        /// <summary>
        /// Gets or sets the price usd.
        /// </summary>
        /// <value>
        /// The price usd.
        /// </value>
        public decimal? PriceDifferential { get; set; }

        /// <summary>
        /// Gets or sets the final price.
        /// </summary>
        /// <value>
        /// The final price.
        /// </value>
        public decimal? Fixation { get; set; }

        /// <summary>
        /// Gets or sets the fixation date.
        /// </summary>
        /// <value>
        /// The fixation date.
        /// </value>
        public DateTime? FixationDate { get; set; }

        /// <summary>
        /// Gets or sets the terms.
        /// </summary>
        /// <value>
        /// The terms.
        /// </value>
        public string Terms { get; set; }

        /// <summary>
        /// Gets or sets the weights.
        /// </summary>
        /// <value>
        /// The weights.
        /// </value>
        public string Weights { get; set; }

        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        /// <value>
        /// The payment.
        /// </value>
        public string Payment { get; set; }

        /// <summary>
        /// Gets or sets the samples.
        /// </summary>
        /// <value>
        /// The samples.
        /// </value>
        public string Samples { get; set; }

        /// <summary>
        /// Gets or sets the arbitration.
        /// </summary>
        /// <value>
        /// The arbitration.
        /// </value>
        public string Arbitration { get; set; }

        /// <summary>
        /// Gets or sets the others.
        /// </summary>
        /// <value>
        /// The others.
        /// </value>
        public string Others { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public virtual Client Client { get; set; }

        /// <summary>
        /// Gets or sets the agent.
        /// </summary>
        /// <value>
        /// The agent.
        /// </value>
        public virtual Agent Agent { get; set; }

        /// <summary>
        /// Gets or sets the seller.
        /// </summary>
        /// <value>
        /// The seller.
        /// </value>
        public virtual Seller Seller { get; set; }
    }
}