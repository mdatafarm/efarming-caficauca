using EFarming.Common;
using EFarming.DTO.AdminModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// PlantationDTO EntityDTO
    /// </summary>
    public class PlantationDTO : EntityDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationDTO"/> class.
        /// </summary>
        public PlantationDTO()
        {
        }

        /// <summary>
        /// Gets or sets the hectares.
        /// </summary>
        /// <value>
        /// The hectares.
        /// </value>
        //public double Hectares { get; set; }

        private string _hectares;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string Hectares
        {
            get { return _hectares; }
            set { _hectares = value.Replace(".", ","); }
        }

        private string _treesDistance;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string TreesDistance
        {
            get { return _treesDistance; }
            set { _treesDistance = value.Replace(".", ","); }
        }

        private string _grooveDistance;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string GrooveDistance
        {
            get { return _grooveDistance; }
            set { _grooveDistance = value.Replace(".", ","); }
        }

        private string _density;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string Density
        {
            get { return _density; }
            set { _density = value.Replace(".", ","); }
        }

        private string _estimatedProduction;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string EstimatedProduction
        {
            get { return _estimatedProduction; }
            set { _estimatedProduction = value.Replace(".", ","); }
        }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        /// <value>
        /// The age.
        /// </value>
        public DateTime Age { get; set; }

        /// <summary>
        /// Gets or sets the number of plants.
        /// </summary>
        /// <value>
        /// The number of plants.
        /// </value>
        public int NumberOfPlants { get; set; }

        /// <summary>
        /// Gets or sets the plantation status identifier.
        /// </summary>
        /// <value>
        /// The plantation status identifier.
        /// </value>
        public Guid PlantationStatusId { get; set; }

        /// <summary>
        /// Gets or sets the productivity identifier.
        /// </summary>
        /// <value>
        /// The productivity identifier.
        /// </value>
        public Guid ProductivityId { get; set; }

        /// <summary>
        /// Gets or sets the plantation type identifier.
        /// </summary>
        /// <value>
        /// The plantation type identifier.
        /// </value>
        public Guid PlantationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the plantation variety identifier.
        /// </summary>
        /// <value>
        /// The plantation variety identifier.
        /// </value>
        public Guid PlantationVarietyId { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the plantation status.
        /// </summary>
        /// <value>
        /// The plantation status.
        /// </value>
        public PlantationStatusDTO PlantationStatus { get; set; }

        /// <summary>
        /// Gets or sets the productivity.
        /// </summary>
        /// <value>
        /// The productivity.
        /// </value>
        public ProductivityDTO Productivity { get; set; }

        /// <summary>
        /// Gets or sets the type of the plantation.
        /// </summary>
        /// <value>
        /// The type of the plantation.
        /// </value>
        public PlantationTypeDTO PlantationType { get; set; }

        /// <summary>
        /// Gets or sets the flowering periods.
        /// </summary>
        /// <value>
        /// The flowering periods.
        /// </value>
        public ICollection<FloweringPeriodDTO> FloweringPeriods { get; set; }

        /// <summary>
        /// Gets or sets the plantation variety.
        /// </summary>
        /// <value>
        /// The plantation variety.
        /// </value>
        public PlantationVarietyDTO PlantationVariety { get; set; }
        
        public int NumberLot { get; set; }

        public Guid MuniLot { get; set; }

        public Guid VillageLot { get; set; }

        public string NomLot { get; set; }
        
        public string LabLot { get; set; }
        
        public string TipoLot { get; set; }
        
        public string FormLot { get; set; }
        
        public int NumEjeArbLot { get; set; }


        private string _estimatedProductionManual;

        /// <summary>
        /// 
        /// </summary>
        public string EstimatedProductionManual
        {
            get { return _estimatedProductionManual; }
            
            set { _estimatedProductionManual = value == null ? "" : value.Replace(".", ","); }
            
        }
    }
}
