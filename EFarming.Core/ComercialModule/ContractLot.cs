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
    /// ContractLot
    /// </summary>
    public class ContractLot : Entity
    {
        /// <summary>
        /// Gets or sets the lot reference.
        /// </summary>
        /// <value>
        /// The lot reference.
        /// </value>
        [Required]
        public string LotReference { get; set; }
        /// <summary>
        /// Gets or sets the icon mark.
        /// </summary>
        /// <value>
        /// The icon mark.
        /// </value>
        [MaxLength(15)]
        public string IcoMark { get; set; }
        /// <summary>
        /// Gets or sets the merchandise description.
        /// </summary>
        /// <value>
        /// The merchandise description.
        /// </value>
        [MaxLength(250)]
        public string MerchandiseDescription { get; set; }
        /// <summary>
        /// Gets or sets the shipment identifier.
        /// </summary>
        /// <value>
        /// The shipment identifier.
        /// </value>
        [Display(Name = "Shipment")]
        public Guid ShipmentId { get; set; }
        /// <summary>
        /// Gets or sets the agreement identifier.
        /// </summary>
        /// <value>
        /// The agreement identifier.
        /// </value>
        [Display(Name = "Contract")]
        public Guid AgreementId { get; set; }
        /// <summary>
        /// Gets or sets the shipment.
        /// </summary>
        /// <value>
        /// The shipment.
        /// </value>
        public virtual Shipment Shipment { get; set; }
        /// <summary>
        /// Gets or sets the agreement.
        /// </summary>
        /// <value>
        /// The agreement.
        /// </value>
        public virtual Agreement Agreement { get; set; }
        /// <summary>
        /// Gets or sets the reference relation ship.
        /// </summary>
        /// <value>
        /// The reference relation ship.
        /// </value>
        public virtual ICollection<ReferenceRelationShip> ReferenceRelationShip { get; set; }
    }
}
