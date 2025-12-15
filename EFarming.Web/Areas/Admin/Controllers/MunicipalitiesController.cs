using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Municipalities Controller
    /// </summary>
    public class MunicipalitiesController : AdminController<MunicipalityDTO, Municipality>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IMunicipalityManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="MunicipalitiesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public MunicipalitiesController(MunicipalityManager manager) : base(manager){
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified current filter.
        /// </summary>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="currentDepartment">The current department.</param>
        /// <param name="searchDepartment">The search department.</param>
        /// <param name="page">The page.</param>
        /// <returns>The View</returns>
        public ViewResult Index(string currentFilter, string searchString, string currentDepartment, string searchDepartment, int? page)
        {
            var withFilter = (!string.IsNullOrEmpty(searchString) || !string.IsNullOrEmpty(searchDepartment));
            if (withFilter)
                page = 1;
            else{
                searchString = currentFilter;
                searchDepartment = currentDepartment;
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentDepartment = searchDepartment;
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var departmentId = string.IsNullOrEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);
            IPagedList<MunicipalityDTO> municipalities;

            if (withFilter){
                municipalities = _manager
                    .GetAll(MunicipalitySpecification.FilterMunicipalities(departmentId, searchString), d => d.Name)
                    .ToPagedList(pageNumber, pageSize);
            }
            else
                municipalities = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);
            
            return View(municipalities);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The View
        /// </returns>
        public override ActionResult Edit(Guid id){
            var municipality = _manager.Get(id);
            ViewBag.CurrentDepartment = municipality.DepartmentId;
            return View(municipality);
        }
    }
}
