using EFarming.Core.AdminModule.FarmStatusAggregate;
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
    /// FarmStatuses Controller
    /// </summary>
    public class FarmStatusesController : AdminController<FarmStatusDTO, FarmStatus>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmStatusManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmStatusesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FarmStatusesController(FarmStatusManager manager)
	  : base(manager)
        {
	  _manager = manager;
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
	  if (searchString != null)
	      page = 1;
	  else
	      searchString = currentFilter;

	  ViewBag.CurrentFilter = searchString;
	  int pageSize = 15;
	  int pageNumber = (page ?? 1);
	  IPagedList<FarmStatusDTO> farmStatuses;
	  if (!string.IsNullOrEmpty(searchString))
	  {
	      farmStatuses = _manager
		.GetAll(FarmStatusSpecification.FilterByName(searchString), d => d.Name)
		.ToPagedList(pageNumber, pageSize);
	  }
	  else
	      farmStatuses = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);

	  return View(farmStatuses);
        }
    }
}