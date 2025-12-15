using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controler for manage the geoposition information about Ssustainability
    /// </summary>
    [CustomAuthorize(Roles = "Sustainability")]
    public class SustainabilityController : BaseController
    {


        /// <summary>
        /// The _farm manager
        /// </summary>
        private IFarmManager _farmManager;
        /// <summary>
        /// The _cooperative manager
        /// </summary>
        private ICooperativeManager _cooperativeManager;
        /// <summary>
        /// The _department manager
        /// </summary>
        private IDepartmentManager _departmentManager;
        /// <summary>
        /// The _municipality manager
        /// </summary>
        private IMunicipalityManager _municipalityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SustainabilityController"/> class.
        /// </summary>
        /// <param name="farmManager">The farm manager.</param>
        /// <param name="cooperativeManager">The cooperative manager.</param>
        /// <param name="departmentManager">The department manager.</param>
        /// <param name="municipalityManager">The municipality manager.</param>
        public SustainabilityController(FarmManager farmManager, ICooperativeManager cooperativeManager, IDepartmentManager departmentManager, IMunicipalityManager municipalityManager)
        {
	  _farmManager = farmManager;
	  _cooperativeManager = cooperativeManager;
	  _departmentManager = departmentManager;
	  _municipalityManager = municipalityManager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Geopositions the farms.
        /// </summary>
        /// <returns></returns>
        public ActionResult GeopositionFarms()
        {	   
            List<Farm> farms;
	  farms = _farmManager.GetAllQueryable(FarmSpecification.FilterWithFarmerInfo("", "", Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty,Guid.Empty, Guid.Empty, ""), f => f.Code).Take(200).ToList();

	  ICollection<EFarming.DTO.AdminModule.CooperativeDTO> cooperatives = _cooperativeManager.GetAll();
	  ViewBag.Cooperatives = cooperatives;

	  ICollection<EFarming.DTO.AdminModule.DepartmentDTO> departments = _departmentManager.GetAll();
	  ViewBag.Departments = departments;

	  ICollection<EFarming.DTO.AdminModule.MunicipalityDTO> municipalites = _municipalityManager.GetAll();
	  ViewBag.Municipalities = municipalites;

	  return View(farms);
        }

        /// <summary>
        /// Searches the farm by the name
        /// </summary>
        /// <returns>List of Locations</returns>
        [HttpPost]
        public ActionResult SearchFarm(string Buscar)
        {
	  List<Farm> farms;
	  farms = _farmManager.GetAllQueryable(FarmSpecification.FilterWithFarmerInfo("", Buscar, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, ""), f => f.Code).Take(200).ToList();	  
	  List<Location> locations = new List<Location>();
	  mapFarmResponse(farms, locations);
	  return Json(locations, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the farm by code.
        /// </summary>
        /// <param name="codeFarm">The _code.</param>
        /// <returns>List of Farm with that code</returns>
        [HttpPost]
        public ActionResult SearchFarmByCode(string codeFarm)
        {
	  List<Farm> farms = _farmManager.GetAllQueryable(FarmSpecification.ByExactCode(codeFarm), f => f.Code).ToList();
	  List<Location> locations = new List<Location>();
	  mapFarmResponse(farms, locations);
	  return Json(locations, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches the farms by department.
        /// </summary>
        /// <param name="_idDepartment">The _id department.</param>
        /// <returns>List of Locations</returns>
        [HttpPost]
        public ActionResult SearchByDepartment(Guid _idDepartment) { 
	  List<Farm> farms = _farmManager.GetAllQueryable(FarmSpecification.FilterWithFarmerInfo("", "", Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, _idDepartment, ""), f => f.Code).ToList();
	  List<Location> locations = new List<Location>();
	  mapFarmResponse(farms,locations);
	  return Json(locations, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Searches farms by cooperative.
        /// </summary>
        /// <param name="_idmuni">The _id cooperative.</param>
        /// <returns>List of Locations</returns>
        [HttpPost]
        public ActionResult SearchByMunicipalyti(Guid _idmuni)
        {
	  try
	  {
	      List<Farm> farms = _farmManager.GetAllQueryable(FarmSpecification.FilterWithFarmerInfo("", "", Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, _idmuni, Guid.Empty, ""), f => f.Code).ToList();
	      List<Location> locations = new List<Location>();
	      mapFarmResponse(farms, locations);
	      return Json(locations, JsonRequestBehavior.AllowGet);
	  }
	  catch (Exception ex)
	  {	      
	      throw;
	  }
	  
        }

        /// <summary>
        /// Maps the farm response.
        /// </summary>
        /// <param name="farms">The farms.</param>
        /// <param name="locations">The locations.</param>
        private static void mapFarmResponse(List<Farm> farms, List<Location> locations)
        {
	  foreach (var farm in farms)
	  {
	      locations.Add(
	      new Location
	      {
		id = farm.Id,
		Name = farm.Name,
		Code = farm.Code,
		Vereda = farm.Village.Name,
		Cooperative = farm.Cooperative.Name,
		Latitude = Convert.ToDouble(farm.GeoLocation.Latitude),
		Longitude = Convert.ToDouble(farm.GeoLocation.Longitude),
		Altitude = Convert.ToInt32(farm.GeoLocation.Elevation),
		url_farm = ("/Farms/Edit/" + farm.Id),
		url_farm_dashboard = ("/Farms/Dashboard/" + farm.Id)
	      });
	  }
        }

        /// <summary>
        /// Clase para Mapear las locaciones. 
        /// </summary>
        public class Location
        {
	  /// <summary>
	  /// Gets or sets the identifier.
	  /// </summary>
	  /// <value>
	  /// The identifier.
	  /// </value>
	  public Guid id { get; set; }
	  /// <summary>
	  /// Gets or sets the name.
	  /// </summary>
	  /// <value>
	  /// The name.
	  /// </value>
	  public string Name { get; set; }
	  /// <summary>
	  /// Gets or sets the code.
	  /// </summary>
	  /// <value>
	  /// The code.
	  /// </value>
	  public string Code { get; set; }
	  /// <summary>
	  /// Gets or sets the vereda.
	  /// </summary>
	  /// <value>
	  /// The vereda.
	  /// </value>
	  public string Vereda { get; set; }
	  /// <summary>
	  /// Gets or sets the cooperative.
	  /// </summary>
	  /// <value>
	  /// The cooperative.
	  /// </value>
	  public string Cooperative { get; set; }
	  /// <summary>
	  /// Gets or sets the latitude.
	  /// </summary>
	  /// <value>
	  /// The latitude.
	  /// </value>
	  public double Latitude { get; set; }
	  /// <summary>
	  /// Gets or sets the longitude.
	  /// </summary>
	  /// <value>
	  /// The longitude.
	  /// </value>
	  public double Longitude { get; set; }
	  /// <summary>
	  /// Gets or sets the altitude.
	  /// </summary>
	  /// <value>
	  /// The altitude.
	  /// </value>
	  public int Altitude { get; set; }
	  /// <summary>
	  /// Gets or sets the url_farm.
	  /// </summary>
	  /// <value>
	  /// The url_farm.
	  /// </value>
	  public string url_farm { get; set; }
	  /// <summary>
	  /// Gets or sets the url_farm_dashboard.
	  /// </summary>
	  /// <value>
	  /// The url_farm_dashboard.
	  /// </value>
	  public string url_farm_dashboard { get; set; }
        }
    }
}