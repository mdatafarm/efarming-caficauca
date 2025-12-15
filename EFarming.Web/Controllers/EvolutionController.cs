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
//    /// Controller for see the evolution of the farm
//    /// </summary>
//    [CustomAuthorize(Roles = "User,Manager")]
//    public class EvolutionController : BaseController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IDashboardManager _manager;
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
//        /// Initializes a new instance of the <see cref="EvolutionController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        /// <param name="countryManager">The country manager.</param>
//        /// <param name="supplierManager">The supplier manager.</param>
//        /// <param name="supplyChainManager">The supply chain manager.</param>
//        /// <param name="farmManager">The farm manager.</param>
//        public EvolutionController(
//            IDashboardManager manager,
//            ICountryManager countryManager,
//            ISupplierManager supplierManager,
//            ISupplyChainManager supplyChainManager,
//            IFarmManager farmManager)
//        {
//            _manager = manager;
//            _countryManager = countryManager;
//            _supplierManager = supplierManager;
//            _supplyChainManager = supplyChainManager;
//            _farmManager = farmManager;
//        }

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns>the view</returns>
//        [HttpGet]
//        public ActionResult Index()
//        {
//            ViewBag.Countries = _countryManager.GetAll();
//            return View();
//        }

//        /// <summary>
//        /// Countries the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>country</returns>
//        public ActionResult Country(Guid id)
//        {
//            var country = _countryManager.Get(id);
//            ViewBag.CountryId = id;
//            ViewBag.Location = country.Name;
//            ViewBag.Suppliers = _supplierManager.GetAll(SupplierSpecification.Filter(string.Empty, id), s => s.Name);
//            return View(country);
//        }

//        /// <summary>
//        /// Suppliers the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>the supplier</returns>
//        public ActionResult Supplier(Guid id)
//        {
//            var supplier = _supplierManager.Get(id);
//            ViewBag.SupplierId = id;
//            ViewBag.Location = supplier.Name;
//            ViewBag.Country = supplier.CountryId;
//            ViewBag.SupplierChains = _supplyChainManager.GetAll(SupplyChainSpecification.Filter(string.Empty, id), s => s.Name);
//            return View(supplier);
//        }

//        /// <summary>
//        /// Suppliers the chain.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>the supplierChain</returns>
//        public ActionResult SupplierChain(Guid id)
//        {
//            var supplierChain = _supplyChainManager.Get(id);
//            ViewBag.SupplierChainId = id;
//            ViewBag.Location = supplierChain.Name;
//            ViewBag.Supplier = supplierChain.SupplierId;
//            ViewBag.Farms = _farmManager.GetAll(FarmSpecification.FilterDashboard(id, null, null), f => f.Id)
//                .OrderBy(f => f.Village.Municipality.Department.Name)
//                .ThenBy(f => f.Village.Municipality.Name)
//                .ThenBy(f => f.Village.Name)
//                .ThenBy(f => f.Name)
//                .ToList();
//            return View(supplierChain);
//        }
//    }
//}
