//using EFarming.Common;
//using EFarming.Core.AdminModule.CountryAggregate;
//using EFarming.Core.AdminModule.SupplierAggregate;
//using EFarming.Core.AdminModule.SupplyChainAggregate;
//using EFarming.Core.FarmModule.FarmAggregate;
//using EFarming.Manager.Contract;
//using EFarming.Manager.Contract.AdminModule;
//using EFarming.Web.Models;
//using System;
//using System.Linq;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
//    /// <summary>
//    /// Controller for manage all the GAP Dashboard Information
//    /// </summary>
//    [CustomAuthorize(Roles = "User,Manager")]
//    public class GAPController : Controller
//    {
//        /// <summary>
//        /// The _country manager
//        /// </summary>
//        private ICountryManager _countryManager;
//        /// <summary>
//        /// The _supplier manager
//        /// </summary>
//        private ISupplierManager _supplierManager;
//        /// <summary>
//        /// The _supply chain manager
//        /// </summary>
//        private ISupplyChainManager _supplyChainManager;
//        /// <summary>
//        /// The _farm manager
//        /// </summary>
//        private IFarmManager _farmManager;
//        /// <summary>
//        /// The _dashboard manager
//        /// </summary>
//        private IDashboardManager _dashboardManager;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="GAPController"/> class.
//        /// </summary>
//        /// <param name="countryManager">The country manager.</param>
//        /// <param name="supplierManager">The supplier manager.</param>
//        /// <param name="supplyChainManager">The supply chain manager.</param>
//        /// <param name="farmRepository">The farm repository.</param>
//        /// <param name="dashboardManager">The dashboard manager.</param>
//        public GAPController(
//	  ICountryManager countryManager,
//	  ISupplierManager supplierManager,
//	  ISupplyChainManager supplyChainManager,
//	  IFarmManager farmRepository,
//	  IDashboardManager dashboardManager)
//        {
//	  _countryManager = countryManager;
//	  _supplierManager = supplierManager;
//	  _supplyChainManager = supplyChainManager;
//	  _farmManager = farmRepository;
//	  _dashboardManager = dashboardManager;
//        }

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns>The View</returns>
//        public ActionResult Index()
//        {
//	  ViewBag.Countries = _dashboardManager.GetGroupedFarms(null, null, null)
//	      .GroupBy(f => f.SupplyChain.Supplier.Country, new EntityComparer<Country>());
//	  ViewBag.CountFarms = _farmManager.Find().Count();
//	  ViewBag.TotalArea = _farmManager.Find().Sum(f => f.Productivity.TotalHectares);
//	  ViewBag.PlantationInformation = _dashboardManager.GetPlantations();
//	  return View();
//        }

//        /// <summary>
//        /// Countries the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The View with country</returns>
//        public ActionResult Country(Guid id)
//        {
//	  var country = _countryManager.Get(id);
//	  ViewBag.CountryId = id;
//	  ViewBag.Location = country.Name;
//	  ViewBag.Suppliers = _dashboardManager.GetGroupedFarms(id, null, null)
//	      .GroupBy(f => f.SupplyChain.Supplier, new EntityComparer<Supplier>());
//	  var farms = _farmManager.GetAll(FarmSpecification.FilterDashboard(null, null, id), f => f.Id);
//	  ViewBag.CountFarms = farms.Count();
//	  ViewBag.TotalArea = farms.Sum(f => f.Productivity.TotalHectares);
//	  return View(country);
//        }

//        /// <summary>
//        /// Suppliers the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The view with supplier</returns>
//        public ActionResult Supplier(Guid id)
//        {
//	  var supplier = _supplierManager.Get(id);
//	  ViewBag.SupplierId = id;
//	  ViewBag.Location = supplier.Name;
//	  ViewBag.Country = supplier.CountryId;
//	  ViewBag.SupplierChains = _dashboardManager.GetGroupedFarms(null, id, null)
//	      .GroupBy(f => f.SupplyChain, new EntityComparer<SupplyChain>());
//	  var farms = _farmManager.GetAll(FarmSpecification.FilterDashboard(null, id, null), f => f.Id);
//	  ViewBag.CountFarms = farms.Count();
//	  ViewBag.TotalArea = farms.Sum(f => f.Productivity.TotalHectares);
//	  return View(supplier);
//        }

//        /// <summary>
//        /// Suppliers the chain.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The View with supplierChain</returns>
//        public ActionResult SupplierChain(Guid id)
//        {
//	  var supplierChain = _supplyChainManager.Get(id);
//	  ViewBag.SupplierChainId = id;
//	  ViewBag.Location = supplierChain.Name;
//	  ViewBag.Supplier = supplierChain.SupplierId;
//	  var farms = _farmManager.GetAll(FarmSpecification.FilterDashboard(id, null, null), f => f.Id)
//	      .OrderBy(f => f.Village.Municipality.Department.Name)
//	      .ThenBy(f => f.Village.Municipality.Name)
//	      .ThenBy(f => f.Village.Name)
//	      .ThenBy(f => f.Name)
//	      .ToList();
//	  ViewBag.Farms = farms;
//	  ViewBag.CountFarms = farms.Count();
//	  ViewBag.TotalArea = farms.Sum(f => f.Productivity.TotalHectares);
//	  return View(supplierChain);
//        }
//    }
//}