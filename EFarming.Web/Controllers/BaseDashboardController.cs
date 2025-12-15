using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Manager.Contract.AdminModule;
using System;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// This controle is used to manage the home dashboards
    /// </summary>
    public class BaseDashboardController : BaseController
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
        /// Initializes a new instance of the <see cref="BaseDashboardController"/> class.
        /// </summary>
        /// <param name="countryManager">The country manager.</param>
        /// <param name="supplierManager">The supplier manager.</param>
        /// <param name="supplyChainManager">The supply chain manager.</param>
        public BaseDashboardController(ICountryManager countryManager,
	  ISupplierManager supplierManager,
	  ISupplyChainManager supplyChainManager)
        {
	  _countryManager = countryManager;
	  _supplierManager = supplierManager;
	  _supplyChainManager = supplyChainManager;
        }

        /// <summary>
        /// Fills the selects.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="supplier">The supplier.</param>
        /// <param name="supplyChain">The supply chain.</param>
        protected void FillSelects(Guid? country = null, Guid? supplier = null, Guid? supplyChain = null)
        {
	  ViewBag.Countries = new SelectList(_countryManager.GetAll(), "Id", "Name", country);
	  ViewBag.Suppliers = new SelectList(_supplierManager.GetAll(SupplierSpecification.Filter(string.Empty, country), s => s.Name), "Id", "Name", supplier);
	  ViewBag.SupplyChains = new SelectList(_supplyChainManager.GetAll(SupplyChainSpecification.Filter(string.Empty, supplier), sc => sc.Name), "Id", "Name", supplyChain);

	  if (supplyChain.HasValue)
	  {
	      ViewBag.Action = "SupplierChain";
	      ViewBag.Id = supplyChain.Value;
	  }
	  else if (supplier.HasValue)
	  {
	      ViewBag.Action = "Supplier";
	      ViewBag.Id = supplier.Value;
	  }
	  else if (country.HasValue)
	  {
	      ViewBag.Action = "Country";
	      ViewBag.Id = country.Value;
	  }
	  else
	  {
	      ViewBag.Action = "Index";
	      ViewBag.Id = string.Empty;
	  }
        }
    }
}