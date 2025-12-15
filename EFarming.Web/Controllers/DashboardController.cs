using EFarming.Manager.Contract.AdminModule;
using EFarming.Web.Models;
using System;
using System.Web.Mvc;


namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Dashboard Controller
    /// </summary>
    [CustomAuthorize(Roles = "Reports")]
    public class DashboardController : BaseDashboardController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="countryManager">The country manager.</param>
        /// <param name="supplierManager">The supplier manager.</param>
        /// <param name="supplyChainManager">The supply chain manager.</param>
        public DashboardController(ICountryManager countryManager,ISupplierManager supplierManager,ISupplyChainManager supplyChainManager)
            : base(countryManager, supplierManager, supplyChainManager) { }

        /// <summary>
        /// Indexes the specified country.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="supplier">The supplier.</param>
        /// <param name="supplyChain">The supply chain.</param>
        /// <returns></returns>
        public ActionResult Index(Guid? country = null, Guid? supplier = null, Guid? supplyChain = null)
        {
            FillSelects(country, supplier, supplyChain);
            return View();
        }
    }
}