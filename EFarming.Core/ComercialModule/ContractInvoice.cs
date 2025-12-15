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
    public class ContractInvoice : Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier from the Shipment.
        /// </value>
        [Key, ForeignKey("Shipment")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        /// <summary>
        /// Gets or sets the number of the invoice.
        /// </summary>
        /// <value>
        /// The number.
        /// </value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the expocafe receipt date.
        /// </summary>
        /// <value>
        /// The expocafe receipt date.
        /// </value>
        public DateTime CafexportReceiptDate { get; set; }

        /// <summary>
        /// Gets or sets the cafexport payment deadline.
        /// </summary>
        /// <value>
        /// The cafexport payment deadline.
        /// </value>
        public DateTime? CafexportPaymentDeadline { get; set; }

        /// <summary>
        /// Gets or sets the client payment dead line.
        /// </summary>
        /// <value>
        /// The client payment dead line.
        /// </value>
        public DateTime? ClientPaymentDeadLine { get; set; }

        /// <summary>
        /// Gets or sets the cafexport payment date.
        /// </summary>
        /// <value>
        /// The cafexport payment date.
        /// </value>
        public DateTime? CafexportPaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the client payment date.
        /// </summary>
        /// <value>
        /// The client payment date.
        /// </value>
        public DateTime? ClientPaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the shipment.
        /// </summary>
        /// <value>
        /// The shipment.
        /// </value>
        public virtual Shipment Shipment { get; set; }
    }
}
