using EFarming.Common;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.QualityModule.QualityProfileAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.SupplyChainAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public class SupplyChain : Entity
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
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the potencial.
        /// </summary>
        /// <value>
        /// The potencial.
        /// </value>
        public double Potencial { get; set; }

        /// <summary>
        /// Gets or sets the bags.
        /// </summary>
        /// <value>
        /// The bags.
        /// </value>
        public double Bags { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        [Required]
        public Guid SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the quality profile identifier.
        /// </summary>
        /// <value>
        /// The quality profile identifier.
        /// </value>
        public Guid? QualityProfileId { get; set; }

        /// <summary>
        /// Gets or sets the supply chain status identifier.
        /// </summary>
        /// <value>
        /// The supply chain status identifier.
        /// </value>
        public Guid? SupplyChainStatusId { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public Guid? DepartmentId { get; set; }

        public int? Code { get; set; }

        public string Address { get; set; }



        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        /// <value>
        /// The supplier.
        /// </value>
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Gets or sets the quality profile.
        /// </summary>
        /// <value>
        /// The quality profile.
        /// </value>
        public virtual QualityProfile QualityProfile { get; set; }

        /// <summary>
        /// Gets or sets the supply chain status.
        /// </summary>
        /// <value>
        /// The supply chain status.
        /// </value>
        public virtual SupplyChainStatus SupplyChainStatus { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public virtual Department Department { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public virtual ICollection<Farm> Farms { get; set; }
    }
}
