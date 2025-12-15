using EFarming.Common;
using EFarming.DTO.FarmModule;
using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;

namespace EFarming.DTO.AdminModule
{
    /// <summary>
    /// SupplyChainDTO EntityDTO
    /// </summary>
    public class SupplyChainDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

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
        /// Gets or sets the supply chain status.
        /// </summary>
        /// <value>
        /// The supply chain status.
        /// </value>
        public SupplyChainStatusDTO SupplyChainStatus { get; set; }

        /// <summary>
        /// Gets or sets the quality profile.
        /// </summary>
        /// <value>
        /// The quality profile.
        /// </value>
        public QualityProfileDTO QualityProfile { get; set; }

        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        /// <value>
        /// The supplier.
        /// </value>
        public SupplierDTO Supplier { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public DepartmentDTO Department { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public List<FarmDTO> Farms { get; set; }
    }
}
