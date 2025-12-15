using EFarming.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.DTO.ImpactModule
{
    /// <summary>
    /// CriteriaDTO EntityDTO
    /// </summary>
    public class CriteriaDTO : EntityDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaDTO"/> class.
        /// </summary>
        public CriteriaDTO()
        {
            if (CriteriaOptions == null)
            {
                CriteriaOptions = new List<CriteriaOptionDTO>();
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CriteriaDTO"/> is mandatory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mandatory; otherwise, <c>false</c>.
        /// </value>
        public bool Mandatory { get; set; }

        /// <summary>
        /// Gets or sets the requirement identifier.
        /// </summary>
        /// <value>
        /// The requirement identifier.
        /// </value>
        public Guid? RequirementId { get; set; }

        /// <summary>
        /// Gets or sets the indicator identifier.
        /// </summary>
        /// <value>
        /// The indicator identifier.
        /// </value>
        public Guid IndicatorId { get; set; }

        /// <summary>
        /// Gets or sets the requirement.
        /// </summary>
        /// <value>
        /// The requirement.
        /// </value>
        public RequirementDTO Requirement { get; set; }

        /// <summary>
        /// Gets or sets the indicator.
        /// </summary>
        /// <value>
        /// The indicator.
        /// </value>
        public IndicatorDTO Indicator { get; set; }

        /// <summary>
        /// Gets or sets the criteria options.
        /// </summary>
        /// <value>
        /// The criteria options.
        /// </value>
        public ICollection<CriteriaOptionDTO> CriteriaOptions { get; set; }

        /// <summary>
        /// Gets the ordered criteria options.
        /// </summary>
        /// <value>
        /// The ordered criteria options.
        /// </value>
        public ICollection<CriteriaOptionDTO> OrderedCriteriaOptions
        {
            get
            {
                return CriteriaOptions.OrderByDescending(co => co.Value).ToList();
            }
        }
    }
}
