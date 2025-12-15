using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Cooperatives Controller
    /// </summary>
    public class SupplyChainsController : AdminController<SupplyChainDTO, SupplyChain>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private ISupplyChainManager _manager;
        private ISupplierManager _managers;
        UnitOfWork db = new UnitOfWork();

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperativesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public SupplyChainsController(SupplyChainManager manager, SupplierManager man) : base(manager)
        {
            _manager = manager;
            _managers = man;
          
        }

        /// <summary>
        /// Indexes the specified current filter.
        /// </summary>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="page">The page.</param>
        /// <returns>The View</returns>
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            var withFilter = (!string.IsNullOrEmpty(searchString));
            if (withFilter)
                page = 1;
            else
            {
                searchString = currentFilter;
            }
         

            var suppliers = _managers.GetAll(SupplierSpecification.Filter("SAN GIL", null), d => d.Name)
                                    .FirstOrDefault();

            ViewBag.CurrentDepartment = null;
            ViewBag.suppliers = suppliers;
            ViewBag.CurrentFilter = searchString;
            ViewBag.Dep= db.Departments.FirstOrDefault();

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            IPagedList<SupplyChainDTO> supplychains;
            if (!string.IsNullOrEmpty(searchString))
            {
                supplychains = _manager
                                    .GetAll(SupplyChainSpecification.Filter(searchString,null), d => d.Name)
                                    .ToPagedList(pageNumber, pageSize);
            }
            else
            {
                supplychains = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);
            }
            return View(supplychains);
        }
        public override ActionResult Edit(Guid id)
        {
            var SupplyChain = _manager.Get(id);
            ViewBag.CurrentDepartment = SupplyChain.DepartmentId;
            var suppliers = _managers.GetAll(SupplierSpecification.Filter(null, new Guid("0AD0F03C-D3BB-409E-B717-ABF8EA2A77E4")), d => d.CountryId)
                                    .ToList();
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name", SupplyChain.SupplierId);
            ViewBag.Suppli = suppliers;

            return View(SupplyChain);
        }
        public override ActionResult Create()
        {
            var suppliers = _managers.GetAll(SupplierSpecification.Filter(null, new Guid("0AD0F03C-D3BB-409E-B717-ABF8EA2A77E4")), d => d.CountryId)
                                    .ToList();
            ViewBag.SupplierId = new SelectList(suppliers, "Id", "Name","selectedCountry");
            ViewBag.Suppli = suppliers;
            return View();
        }

    }
}