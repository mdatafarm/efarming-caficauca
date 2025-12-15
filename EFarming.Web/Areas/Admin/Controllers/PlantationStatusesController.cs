using EFarming.Core.AdminModule.PlantationStatusAggregate;
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
    /// PlantationStatuses Controller
    /// </summary>
    public class PlantationStatusesController : AdminController<PlantationStatusDTO, PlantationStatus>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IPlantationStatusManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationStatusesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PlantationStatusesController(PlantationStatusManager manager)
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
        /// <returns></returns>
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
	  if (searchString != null)
	      page = 1;
	  else
	      searchString = currentFilter;

	  ViewBag.CurrentFilter = searchString;
	  int pageSize = 15;
	  int pageNumber = (page ?? 1);
	  IPagedList<PlantationStatusDTO> plantationStatuses;
	  if (!string.IsNullOrEmpty(searchString))
	  {
	      plantationStatuses = _manager
		.GetAll(PlantationStatusSpecification.FilterByName(searchString), d => d.Name)
		.ToPagedList(pageNumber, pageSize);
	  }
	  else
	      plantationStatuses = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);

	  return View(plantationStatuses);
        }
    }
}