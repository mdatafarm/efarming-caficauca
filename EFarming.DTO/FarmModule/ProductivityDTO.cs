using EFarming.Common;
using System;
using System.Collections.Generic;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// ProductivityDTO EntityDTO
    /// </summary>
    public class ProductivityDTO : EntityDTO
    {
        /// <summary>
        /// Gets or sets the total hectares.
        /// </summary>
        /// <value>
        /// The total hectares.
        /// </value>
        private string _totalHectares = "0";
        public string TotalHectares
        {
            get { return _totalHectares; }
            set { _totalHectares = value.Replace(".", ","); }
        }

        /// <summary>
        /// Gets or sets the infrastructure hectares.
        /// </summary>
        /// <value>
        /// The infrastructure hectares.
        /// </value>
        private string _infrastructureHectares = "0";
        public string InfrastructureHectares
        {
            get { return _infrastructureHectares; }
            set { _infrastructureHectares = value.Replace(".", ","); }
        }

        /// <summary>
        /// Gets or sets the forest protected hectares.
        /// </summary>
        /// <value>
        /// The forest protected hectares.
        /// </value>
        private string _forestProtectedHectares = "0";
        public string ForestProtectedHectares
        {
            get { return _forestProtectedHectares; }
            set { _forestProtectedHectares = value.Replace(".", ","); }
        }

        /// <summary>
        /// Gets or sets the conservation hectares.
        /// </summary>
        /// <value>
        /// The conservation hectares.
        /// </value>
        private string _conservationHectares = "0";
        public string ConservationHectares
        {
            get { return _conservationHectares; }
            set { _conservationHectares = value.Replace(".", ","); }
        }

        /// <summary>
        /// Gets or sets the shading percentage.
        /// </summary>
        /// <value>
        /// The shading percentage.
        /// </value>
        private string _shadingPercentage = "0";
        public string ShadingPercentage
        {
            get { return _shadingPercentage; }
            set { _shadingPercentage = value.Replace(".", ","); }
        }

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
        private string _averageDensity = "0";
        public string averageDensity
        {
            get { return _averageDensity; }
            set { _averageDensity = value.Replace(".", ","); }
        }

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
        private string _coffeeArea = "0";
        public string coffeeArea
        {
            get { return _coffeeArea; }
            set { _coffeeArea = value.Replace(".", ","); }
        }

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
        private string _productionArea = "0";
        public string productionArea
        {
            get { return _productionArea; }
            set { _productionArea = value.Replace(".", ","); }
        }

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
        private string _growingArea= "0";
        public string growingArea
        {
            get { return _growingArea; }
            set { _growingArea = value.Replace(".", ","); }
        }

        /// <summary>
        /// Gets or sets the estimated production.
        /// </summary>
        /// <value>
        /// The estimated production.
        /// </value>
        public double estimatedProduction { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public FarmDTO Farm { get; set; }

        /// <summary>
        /// Gets or sets the plantations.
        /// </summary>
        /// <value>
        /// The plantations.
        /// </value>
        public List<PlantationDTO> Plantations { get; set; }
    }
}
