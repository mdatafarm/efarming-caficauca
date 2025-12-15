using EFarming.Common;
using EFarming.DTO.AdminModule;
using EFarming.DTO.ProjectModule;
using EFarming.DTO.SustainabilityModule;
using EFarming.DTO.TraceabilityModule;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DTO.FarmModule
{
    /// <summary>
    /// FarmDTO EntityDTO
    /// </summary>
    public class FarmDTO : EntityDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmDTO"/> class.
        /// </summary>
        public FarmDTO()
        {
            Worker = new WorkerDTO();
            Productivity = new ProductivityDTO();
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// The _geolocation
        /// </summary>
        private DbGeography _geolocation;
        
        /// <summary>
        /// Gets or sets the geo location.
        /// </summary>
        /// <value>
        /// The geo location.
        /// </value>
        public DbGeography GeoLocation
        {
            
            get
            {
                if (_geolocation == null)
                {
                    if (!string.IsNullOrEmpty(_longitude) && !string.IsNullOrEmpty(_latitude))
                    {
                        var position = string.Format("POINT({0} {1})", _longitude, _latitude);
                        _geolocation = DbGeography.FromText(position);
                        //Elevation = _geolocation.Elevation.ToString();
                    }
                }
                return _geolocation;
            }
            set
            {
                if (value != null)
                {
                    Elevation = value.Elevation.ToString();
                    Longitude = value.Longitude.ToString();
                    Latitude = value.Latitude.ToString();
                    _geolocation = value;
                }
            }
        }

        /// <summary>
        /// The _longitude
        /// </summary>
        private string _longitude;
        
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value.Replace(",", "."); }
        }

        /// <summary>
        /// The _latitude
        /// </summary>
        private string _latitude;
        
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public string Latitude
        {
            get { return _latitude; }
            set { _latitude = value.Replace(",", "."); }
        }

        /// <summary>
        /// The _elevation
        /// </summary>
        private string _elevation;
        /// <summary>
        /// Gets or sets the elevation.
        /// </summary>
        /// <value>
        /// The elevation.
        /// </value>
        public string Elevation
        {
            get { return _elevation; }
            set { _elevation = value.Replace(",", "."); }
        }

        /// <summary>
        /// The _current technician
        /// </summary>
        private Guid _currentTechnician;
        
        /// <summary>
        /// Gets or sets the current technician.
        /// </summary>
        /// <value>
        /// The current technician.
        /// </value>
        public Guid CurrentTechnician
        {
            get
            {
                if (_currentTechnician == Guid.Empty)
                {
                    if (AssociatedPeople == null)
                    {
                        _currentTechnician = new Guid("D87A4D23-68D0-4407-AE69-C3CEB082C396") ;
                    }
                    else if (AssociatedPeople.Count() == 0 || AssociatedPeople.First() == null)
                    {
                        _currentTechnician = new Guid("D87A4D23-68D0-4407-AE69-C3CEB082C396") ;
                    }
                    else
                    {
                        _currentTechnician = AssociatedPeople.First().Id;
                    }
                }
                return _currentTechnician;
            }
            set
            {
                _currentTechnician = value;
            }
        }

        #region Dashboard calculations
        /// <summary>
        /// Gets the density indicator.
        /// </summary>
        /// <value>
        /// The density indicator.
        /// </value>
        public double DensityIndicator
        {
            get
            {
                return Plants / Hectares;
            }
        }

        /// <summary>
        /// Gets or sets the plants.
        /// </summary>
        /// <value>
        /// The plants.
        /// </value>
        public int Plants { get; set; }

        /// <summary>
        /// Gets or sets the hectares.
        /// </summary>
        /// <value>
        /// The hectares.
        /// </value>
        public double Hectares { get; set; }

        /// <summary>
        /// Gets the fertilizer indicator.
        /// </summary>
        /// <value>
        /// The fertilizer indicator.
        /// </value>
        public double FertilizerIndicator
        {
            get
            {
                return FertilizerBags * 50000 / ProductivePlants;
            }
        }

        /// <summary>
        /// Gets or sets the productive plants.
        /// </summary>
        /// <value>
        /// The productive plants.
        /// </value>
        public int ProductivePlants { get; set; }

        /// <summary>
        /// Gets or sets the fertilizer bags.
        /// </summary>
        /// <value>
        /// The fertilizer bags.
        /// </value>
        public double FertilizerBags { get; set; }

        /// <summary>
        /// Gets the productivity indicator.
        /// </summary>
        /// <value>
        /// The productivity indicator.
        /// </value>
        public double ProductivityIndicator
        {
            get
            {
                return EstimatedProduction / Hectares;
            }
        }

        /// <summary>
        /// Gets or sets the estimated production.
        /// </summary>
        /// <value>
        /// The estimated production.
        /// </value>
        public double EstimatedProduction { get; set; }

        /// <summary>
        /// Gets or sets the age indicator.
        /// </summary>
        /// <value>
        /// The age indicator.
        /// </value>
        public int AgeIndicator { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets the supply chain identifier.
        /// </summary>
        /// <value>
        /// The supply chain identifier.
        /// </value>
        public Guid? SupplyChainId { get; set; }

        /// <summary>
        /// Gets or sets the village identifier.
        /// </summary>
        /// <value>
        /// The village identifier.
        /// </value>
        public Guid VillageId { get; set; }

        /// <summary>
        /// Gets or sets the farm status identifier.
        /// </summary>
        /// <value>
        /// The farm status identifier.
        /// </value>
        public Guid? FarmStatusId { get; set; }

        /// <summary>
        /// Gets or sets the farm substatus identifier.
        /// </summary>
        /// <value>
        /// The farm substatus identifier.
        /// </value>
        public Guid? FarmSubstatusId { get; set; }

        /// <summary>
        /// Gets or sets the cooperative identifier.
        /// </summary>
        /// <value>
        /// The cooperative identifier.
        /// </value>
        public Guid? CooperativeId { get; set; }

        /// <summary>
        /// Gets or sets the ownership type identifier.
        /// </summary>
        /// <value>
        /// The ownership type identifier.
        /// </value>
        public Guid? OwnershipTypeId { get; set; }

        /// <summary>
        /// Gets or sets the supply chain.
        /// </summary>
        /// <value>
        /// The supply chain.
        /// </value>
        public SupplyChainDTO SupplyChain { get; set; }

        /// <summary>
        /// Gets or sets the village.
        /// </summary>
        /// <value>
        /// The village.
        /// </value>
        public VillageDTO Village { get; set; }

        /// <summary>
        /// Gets or sets the farm substatus.
        /// </summary>
        /// <value>
        /// The farm substatus.
        /// </value>
        public FarmSubstatusDTO FarmSubstatus { get; set; }

        /// <summary>
        /// Gets or sets the cooperative.
        /// </summary>
        /// <value>
        /// The cooperative.
        /// </value>
        public CooperativeDTO Cooperative { get; set; }

        /// <summary>
        /// Gets or sets the type of the ownership.
        /// </summary>
        /// <value>
        /// The type of the ownership.
        /// </value>
        public OwnershipTypeDTO OwnershipType { get; set; }

        /// <summary>
        /// Gets or sets the productivity.
        /// </summary>
        /// <value>
        /// The productivity.
        /// </value>
        public ProductivityDTO Productivity { get; set; }

        /// <summary>
        /// Gets or sets the worker.
        /// </summary>
        /// <value>
        /// The worker.
        /// </value>
        public WorkerDTO Worker { get; set; }

        /// <summary>
        /// Gets or sets the soil analysis.
        /// </summary>
        /// <value>
        /// The soil analysis.
        /// </value>
        public List<SoilAnalysisDTO> SoilAnalysis { get; set; }

        /// <summary>
        /// Gets or sets the fertilizers.
        /// </summary>
        /// <value>
        /// The fertilizers.
        /// </value>
        public List<FertilizerDTO> Fertilizers { get; set; }
        public List<GroupedFertilizer> GroupedFertilizers{ get; set; }

        public class GroupedFertilizer
        {
            public int Year { get; set; }
            public double Quantity { get; set; }
            public double TotalValue { get; set; }
            public double AveragePrice { get; set; }
        }

        /// <summary>
        /// Gets or sets the soil types.
        /// </summary>
        /// <value>
        /// The soil types.
        /// </value>
        public List<SoilTypeDTO> SoilTypes { get; set; }

        /// <summary>
        /// Gets or sets the other activities.
        /// </summary>
        /// <value>
        /// The other activities.
        /// </value>
        public List<FarmOtherActivityDTO> OtherActivities { get; set; }

        /// <summary>
        /// Gets or sets the family unit members.
        /// </summary>
        /// <value>
        /// The family unit members.
        /// </value>
        public List<FamilyUnitMemberDTO> FamilyUnitMembers { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public List<ImageDTO> Images { get; set; }

        /// <summary>
        /// Gets or sets the associated people.
        /// </summary>
        /// <value>
        /// The associated people.
        /// </value>
        public List<UserDTO> AssociatedPeople { get; set; }

        /// <summary>
        /// Gets or sets the invoices.
        /// </summary>
        /// <value>
        /// The invoices.
        /// </value>
        public List<InvoiceDTO> Invoices { get; set; }

        public List<groupedInvoice> GroupedInvoices { get; set; }

        public class groupedInvoice
        {
            public int Year { get; set; }
            public double Totalkg { get; set; }
            public double TotalValue { get; set; }
            public double AverageValue { get; set; }
        }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public List<ProjectDTO> Projects { get; set; }

        public List<ContactDTO> Contacts { get; set; }
    }
}
