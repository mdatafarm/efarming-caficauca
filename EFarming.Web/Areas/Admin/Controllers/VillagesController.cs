using EFarming.Core.AdminModule.VillageAggregate;
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
    /// Villages Controller
    /// </summary>
    public class VillagesController : AdminController<VillageDTO, Village>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IVillageManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="VillagesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public VillagesController(VillageManager manager)
	  : base(manager)
        {
	  _manager = manager;
        }

        /// <summary>
        /// Indexes the specified current filter.
        /// </summary>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="currentDepartment">The current department.</param>
        /// <param name="searchDepartment">The search department.</param>
        /// <param name="currentMunicipality">The current municipality.</param>
        /// <param name="searchMunicipality">The search municipality.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public ViewResult Index(string currentFilter, string searchString, string currentDepartment,
	  string searchDepartment, string currentMunicipality, string searchMunicipality, int? page)
        {
	  var withFilter = (!string.IsNullOrEmpty(searchString)
			  || !string.IsNullOrEmpty(searchDepartment)
			  || !string.IsNullOrEmpty(searchMunicipality));
	  if (withFilter)
	      page = 1;
	  else
	  {
	      searchString = currentFilter;
	      searchMunicipality = currentMunicipality;
	      searchDepartment = currentDepartment;
	  }

	  ViewBag.CurrentFilter = searchString;
	  ViewBag.CurrentMunicipality = searchMunicipality;
	  ViewBag.CurrentDepartment = searchDepartment;
	  int pageSize = 15;
	  int pageNumber = (page ?? 1);
	  var municipalityId = string.IsNullOrEmpty(searchMunicipality) ? Guid.Empty : Guid.Parse(searchMunicipality);
	  var departmentId = string.IsNullOrEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);
	  IPagedList<VillageDTO> villages;

	  if (withFilter)
	  {
	      villages = _manager
		.GetAll(VillageSpecification.FilterVillages(searchString, municipalityId, departmentId), d => d.Name)
		.ToPagedList(pageNumber, pageSize);
	  }
	  else
	      villages = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);
	  return View(villages);
        }
    }
}