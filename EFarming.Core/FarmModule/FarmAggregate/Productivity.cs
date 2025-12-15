using EFarming.Common;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Productivity Entity
    /// </summary>
    public class Productivity : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Productivity"/> class.
        /// </summary>
        public Productivity()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, ForeignKey("Farm")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        /// <summary>
        /// Gets or sets the total hectares.
        /// </summary>
        /// <value>
        /// The total hectares.
        /// </value>
        [MaxLength(20)]
        public string TotalHectares { get; set; }

        /// <summary>
        /// Gets or sets the infrastructure hectares.
        /// </summary>
        /// <value>
        /// The infrastructure hectares.
        /// </value>
        [MaxLength(20)]
        public string InfrastructureHectares { get; set; }

        /// <summary>
        /// Gets or sets the forest protected hectares.
        /// </summary>
        /// <value>
        /// The forest protected hectares.
        /// </value>
        [MaxLength(20)]
        public string ForestProtectedHectares { get; set; }

        /// <summary>
        /// Gets or sets the conservation hectares.
        /// </summary>
        /// <value>
        /// The conservation hectares.
        /// </value>
        [MaxLength(20)]
        public string ConservationHectares { get; set; }

        /// <summary>
        /// Gets or sets the shading percentage.
        /// </summary>
        /// <value>
        /// The shading percentage.
        /// </value>
        [MaxLength(20)]
        public string ShadingPercentage { get; set; }

        /// <summary>
        /// Gets or sets the average age.
        /// </summary>
        /// <value>
        /// The average age.
        /// </value>
        public double averageAge { get; set; }

        /// <summary>
        /// Gets or sets the average density.
        /// </summary>
        /// <value>
        /// The average density.
        /// </value>
        [MaxLength(20)]
        public string averageDensity { get; set; }

        /// <summary>
        /// Gets or sets the percentage colombia.
        /// </summary>
        /// <value>
        /// The percentage colombia.
        /// </value>
        public double percentageColombia { get; set; }
        /// <summary>
        /// Gets or sets the percentage caturra.
        /// </summary>
        /// <value>
        /// The percentage caturra.
        /// </value>
        public double percentageCaturra { get; set; }
        /// <summary>
        /// Gets or sets the percentage castillo.
        /// </summary>
        /// <value>
        /// The percentage castillo.
        /// </value>
        public double percentageCastillo { get; set; }
        /// <summary>
        /// Gets or sets the percentageotra.
        /// </summary>
        /// <value>
        /// The percentageotra.
        /// </value>
        public double percentageotra { get; set; }

        /// <summary>
        /// Gets or sets the coffee area.
        /// </summary>
        /// <value>
        /// The coffee area.
        /// </value>
        [MaxLength(20)]
        public string coffeeArea { get; set; }

        /// <summary>
        /// Gets or sets the production trees.
        /// </summary>
        /// <value>
        /// The production trees.
        /// </value>
        public double productionPlants { get; set; }

        /// <summary>
        /// Gets or sets the production percentage.
        /// </summary>
        /// <value>
        /// The production percentage.
        /// </value>
        public double productionPercentage { get; set; }

        /// <summary>
        /// Gets or sets the production area percentage.
        /// </summary>
        /// <value>
        /// The production area percentage.
        /// </value>
        public double productionAreaPercentage { get; set; }

        /// <summary>
        /// Gets or sets the production area.
        /// </summary>
        /// <value>
        /// The production area.
        /// </value>
        [MaxLength(20)]
        public string productionArea { get; set; }

        /// <summary>
        /// Gets or sets the growing trees.
        /// </summary>
        /// <value>
        /// The growing trees.
        /// </value>
        public double growingPlants { get; set; }

        /// <summary>
        /// Gets or sets the growing percentage.
        /// </summary>
        /// <value>
        /// The growing percentage.
        /// </value>
        public double growingPercentage { get; set; }

        /// <summary>
        /// Gets or sets the growing area percentage.
        /// </summary>
        /// <value>
        /// The growing area percentage.
        /// </value>
        public double growingAreaPercentage { get; set; }

        /// <summary>
        /// Gets or sets the growing area.
        /// </summary>
        /// <value>
        /// The growing area.
        /// </value>
        [MaxLength(20)]
        public string growingArea { get; set; }

        /// <summary>
        /// Gets or sets the estimated production.
        /// </summary>
        /// <value>
        /// The estimated production.
        /// </value>
        public double estimatedProduction { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }

        /// <summary>
        /// Gets or sets the plantations.
        /// </summary>
        /// <value>
        /// The plantations.
        /// </value>
        public virtual ICollection<Plantation> Plantations { get; set; }

        #region public methods
        /// <summary>
        /// Gets the plantation types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlantationType> GetPlantationTypes()
        {
            return Plantations.Select(p => p.PlantationType);
        }
        #endregion
    }
}
