using EFarming.Common;
using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the Overview information
    /// </summary>
    [CustomAuthorize(Roles = "Reports")]
    public class OverviewController : BaseController
    {
        /// <summary>
        /// The _country manager
        /// </summary>
        private ICountryManager _countryManager;
        /// <summary>
        /// The _supplier manager
        /// </summary>
        private ISupplierManager _supplierManager;
        /// <summary>
        /// The _supply chain manager
        /// </summary>
        private ISupplyChainManager _supplyChainManager;
        /// <summary>
        /// The _farm manager
        /// </summary>
        private IFarmManager _farmManager;
        /// <summary>
        /// The _dashboard manager
        /// </summary>
        private IDashboardManager _dashboardManager;

        /// <summary>
        /// The _cooperative manager
        /// </summary>
        private ICooperativeManager _cooperativeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewController"/> class.
        /// </summary>
        /// <param name="countryManager">The country manager.</param>
        /// <param name="supplierManager">The supplier manager.</param>
        /// <param name="supplyChainManager">The supply chain manager.</param>
        /// <param name="farmRepository">The farm repository.</param>
        /// <param name="dashboardManager">The dashboard manager.</param>
        /// <param name="cooperativeManager">The dashboard manager.</param>
        public OverviewController(
            ICountryManager countryManager,
            ISupplierManager supplierManager,
            ISupplyChainManager supplyChainManager,
            IFarmManager farmRepository,
            IDashboardManager dashboardManager,
            ICooperativeManager cooperativeManager)
        {
            _countryManager = countryManager;
            _supplierManager = supplierManager;
            _supplyChainManager = supplyChainManager;
            _farmManager = farmRepository;
            _dashboardManager = dashboardManager;
            _cooperativeManager = cooperativeManager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Index(DateTime? start, DateTime? end)
        {
            if(start == null)
            {
                ViewBag.SelectedStart = DateTime.Now.AddDays(-30);
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                ViewBag.SelectedEnd = DateTime.Now;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }
            
            ViewBag.Countries = _dashboardManager.GetGroupedFarms(null, null, null)
                .GroupBy(f => f.SupplyChain.Supplier.Country, new EntityComparer<Country>());
            ViewBag.CountFarms = _farmManager.CountFarms("World", null);
            ViewBag.TotalArea = _farmManager.TotalArea("World", null);
            ViewBag.PlantationInformation = _dashboardManager.GetPlantations();
            return View();
        }

        /// <summary>
        /// Countries the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="start">The identifier.</param>
        /// <param name="end">The identifier.</param>
        /// <returns>The View with country</returns>
        public ActionResult Country(Guid id, DateTime? start, DateTime? end)
        {
            if (start == null)
            {
                ViewBag.SelectedStart = DateTime.Now.AddDays(-30);
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                ViewBag.SelectedEnd = DateTime.Now;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }
            var country = _countryManager.Get(id);
            ViewBag.CountryId = id;
            ViewBag.Location = country.Name;
            ViewBag.Suppliers = _dashboardManager.GetGroupedFarms(id, null, null)
                .GroupBy(f => f.SupplyChain.Supplier, new EntityComparer<Supplier>());
            ViewBag.CountFarms = _farmManager.CountFarms("Country", id);
            ViewBag.TotalArea = _farmManager.TotalArea("Country", id);
            ViewBag.PlantationInformation = _dashboardManager.GetPlantations();
            return View(country);
        }

        /// <summary>
        /// Suppliers the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View with supplier</returns>
        public ActionResult Supplier(Guid id, DateTime? start, DateTime? end)
        {
            if (start == null)
            {
                ViewBag.SelectedStart = DateTime.Now.AddDays(-30);
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                ViewBag.SelectedEnd = DateTime.Now;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }
            var supplier = _supplierManager.Get(id);
            ViewBag.SupplierId = id;
            ViewBag.Location = supplier.Name;
            ViewBag.Country = supplier.CountryId;
            ViewBag.SupplyChains = _dashboardManager.GetGroupedFarms(null, id, null)
                .GroupBy(f => f.SupplyChain, new EntityComparer<SupplyChain>());
            ViewBag.CountFarms = _farmManager.CountFarms("Supplier", id);
            ViewBag.TotalArea = _farmManager.TotalArea("Supplier", id);
            ViewBag.PlantationInformation = _dashboardManager.GetPlantations();
            return View(supplier);
        }

        /// <summary>
        /// Suppliers the chain.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View with supplierChain</returns>
        public ActionResult SupplierChain(Guid id, DateTime? start, DateTime? end)
        {
            if (start == null)
            {
                ViewBag.SelectedStart = DateTime.Now.AddDays(-30);
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                ViewBag.SelectedEnd = DateTime.Now;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }
            var supplierChain = _supplyChainManager.Get(id);
            ViewBag.SupplierChainId = id;
            ViewBag.cooperatives = _cooperativeManager.GetAll();
            ViewBag.Location = supplierChain.Name;
            ViewBag.Supplier = supplierChain.SupplierId;
            //var farms = _farmManager.GetAll(FarmSpecification.FilterDashboard(id, null, null), f => f.Id)
            //    .OrderBy(f => f.Village.Municipality.Department.Name)
            //    .ThenBy(f => f.Village.Municipality.Name)
            //    .ThenBy(f => f.Village.Name)
            //    .ThenBy(f => f.Name)
            //    .ToList();
            //ViewBag.Farms = farms;
            ViewBag.CountFarms = _farmManager.CountFarms("SupplierChain", id);
            ViewBag.TotalArea = _farmManager.TotalArea("SupplierChain", id);
            ViewBag.PlantationInformation = _dashboardManager.GetPlantations();
            ViewBag.QualityProfile = supplierChain.QualityProfile != null ? supplierChain.QualityProfile.Name : string.Empty;
            return View(supplierChain);
        }

        /// <summary>
        /// Shows the dashboard by cooperative
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Cooperative(Guid id, DateTime? start, DateTime? end)
        {
            if (start == null)
            {
                ViewBag.SelectedStart = DateTime.Now.AddDays(-30);
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                ViewBag.SelectedEnd = DateTime.Now;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }
            var cooperative = _cooperativeManager.Get(id);
            var supplierChain = _supplyChainManager.Get(new Guid("C8BE0458-39C5-40A2-A3AF-31387FD3D14C"));
            ViewBag.SupplierChainId = "C8BE0458-39C5-40A2-A3AF-31387FD3D14C";
            ViewBag.cooperatives = _cooperativeManager.GetAll();
            ViewBag.Location = cooperative.Name;
            ViewBag.CooperativeId = id;
            ViewBag.Supplier = "CALDAS";
            ViewBag.CountFarms = _farmManager.CountFarms("Cooperative", id);
            ViewBag.TotalArea = _farmManager.TotalArea("Cooperative", id);
            ViewBag.PlantationInformation = _dashboardManager.GetPlantations();
            return View(supplierChain);
        }
    }
}