using AutoMapper;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using PagedList;
using System;
using System.Web.Mvc;

namespace EFarming.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Departments Controller
    /// </summary>
    public class DepartmentsController : AdminController<DepartmentDTO, Department>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IDepartmentManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentsController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public DepartmentsController(DepartmentManager manager) : base(manager){
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
            IPagedList<DepartmentDTO> departments;
            if (!string.IsNullOrEmpty(searchString))
                departments = _manager.GetAll(searchString, d => d.Name).ToPagedList(pageNumber, pageSize);
            else
                departments = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);
            
            return View(departments);
        }
    }
}
