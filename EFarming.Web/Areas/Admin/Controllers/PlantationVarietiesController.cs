using EFarming.Core.AdminModule.PlantationVarietyAggregate;
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
    /// PlantationVarieties Controller
    /// </summary>
    public class PlantationVarietiesController : AdminController<PlantationVarietyDTO, PlantationVariety>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IPlantationVarietyManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationVarietiesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PlantationVarietiesController(PlantationVarietyManager manager)
	  : base(manager)
        {
	  _manager = manager;
        }

        /// <summary>
        /// Indexes the specified current filter.
        /// </summary>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="currentPlantationType">Type of the current plantation.</param>
        /// <param name="searchPlantationType">Type of the search plantation.</param>
        /// <param name="page">The page.</param>
        /// <returns>The result</returns>
        public ViewResult Index(string currentFilter, string searchString, string currentPlantationType, string searchPlantationType, int? page)
        {
	  var withFilter = (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(searchPlantationType));
	  if (withFilter)
	      page = 1;
	  else
	  {
	      searchString = currentFilter;
	      searchPlantationType = currentPlantationType;
	  }

	  ViewBag.CurrentFilter = searchString;
	  ViewBag.CurrentPlantationType = searchPlantationType;

	  int pageNumber = (page ?? 1);
	  var plantationTypeId = string.IsNullOrEmpty(searchPlantationType) ? Guid.Empty : Guid.Parse(searchPlantationType);
	  IPagedList<PlantationVarietyDTO> plantationVarieties;

	  if (withFilter)
	  {
	      plantationVarieties = _manager
		.GetAll(PlantationVarietySpecification.FilterPlantationVariety(searchString, plantationTypeId), pv => pv.Name)
		.ToPagedList(pageNumber, PER_PAGE);
	  }
	  else
	  {
	      plantationVarieties = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, PER_PAGE);
	  }
	  return View(plantationVarieties);
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
	  var plantationVariety = _manager.Get(id);
	  ViewBag.CurrentPlantationType = plantationVariety.PlantationTypeId;
	  return View(plantationVariety);
        }
    }
}