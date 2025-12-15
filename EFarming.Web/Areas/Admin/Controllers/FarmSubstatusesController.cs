using EFarming.Core.AdminModule.FarmSubstatusAggregate;
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
    /// FarmSubstatuses Controller
    /// </summary>
    public class FarmSubstatusesController : AdminController<FarmSubstatusDTO, FarmSubstatus>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmSubstatusManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmSubstatusesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FarmSubstatusesController(FarmSubstatusManager manager) : base(manager){
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified current filter.
        /// </summary>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="currentFarmStatus">The current farm status.</param>
        /// <param name="searchFarmStatus">The search farm status.</param>
        /// <param name="page">The page.</param>
        /// <returns>The View</returns>
        public ViewResult Index(string currentFilter, string searchString, string currentFarmStatus, string searchFarmStatus, int? page)
        {
            var withFilter = (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(searchFarmStatus));
            if (withFilter)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                searchFarmStatus = currentFarmStatus;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFarmStatus = searchFarmStatus;
            
            int pageNumber = (page ?? 1);
            var farmStatusId = string.IsNullOrEmpty(searchFarmStatus) ? Guid.Empty : Guid.Parse(searchFarmStatus);
            IPagedList<FarmSubstatusDTO> substatuses;

            if (withFilter)
            {
                substatuses = _manager
                    .GetAll(FarmSubstatusSpecification.FilterFarmSubstatus(searchString, farmStatusId), fss => fss.Name)
                    .ToPagedList(pageNumber, PER_PAGE);
            }
            else
            {
                substatuses = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, PER_PAGE);
            }
            return View(substatuses);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The View
        /// </returns>
        public override ActionResult Edit(Guid id)
        {
            var farmSubstatus = _manager.Get(id);
            ViewBag.CurrentFarmStatus = farmSubstatus.FarmStatusId;
            return View(farmSubstatus);
        }
    }
}
