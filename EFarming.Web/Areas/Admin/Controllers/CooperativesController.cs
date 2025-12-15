using EFarming.Core.AdminModule.CooperativeAggregate;
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
    public class CooperativesController : AdminController<CooperativeDTO, Cooperative>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private ICooperativeManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooperativesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CooperativesController(CooperativeManager manager) : base(manager)
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
            IPagedList<CooperativeDTO> cooperatives;
            if (!string.IsNullOrEmpty(searchString))
            {
                cooperatives = _manager
                                    .GetAll(CooperativeSpecification.FilterByName(searchString), d => d.Name)
                                    .ToPagedList(pageNumber, pageSize);
            }
            else
            {
                cooperatives = _manager.GetAll(d => d.Name).ToPagedList(pageNumber, pageSize);
            }
            return View(cooperatives);
        }
    }
}