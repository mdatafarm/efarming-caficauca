using EFarming.Common;
using EFarming.DTO.FarmModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.DTO.ImpactModule
{
    /// <summary>
    /// IndicatorDTO EntityDTO
    /// </summary>
    public class IndicatorDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public int Scale { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the assessment template identifier.
        /// </summary>
        /// <value>
        /// The assessment template identifier.
        /// </value>
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public CategoryDTO Category { get; set; }

        /// <summary>
        /// Gets or sets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public virtual ICollection<FarmDTO> Farms { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public virtual ICollection<CriteriaDTO> Criteria { get; set; }

        /// <summary>
        /// Gets the ordered criteria options.
        /// </summary>
        /// <value>
        /// The ordered criteria options.
        /// </value>
        public ICollection<CriteriaDTO> OrderedCriteriaOptions
        {
            get
            {
                if (Criteria == null)
                {
                    Criteria = new List<CriteriaDTO>();
                }
                return Criteria.OrderByDescending(co => co.Value).ToList();
            }
        }
    }
}
