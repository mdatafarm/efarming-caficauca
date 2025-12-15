using EFarming.Core.AdminModule.OwnershipTypeAggregate;
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
    /// OwershipTypes Controller
    /// </summary>
    public class OwnershipTypesController : AdminController<OwnershipTypeDTO, OwnershipType>
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IOwnershipTypeManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnershipTypesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public OwnershipTypesController(OwnershipTypeManager manager) : base(manager) {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified current filter.
        /// </summary>
        /// <param name="currentFilter">The current filter.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="page">The page.</param>
        /// <returns>the result</returns>
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
        
            ViewBag.CurrentFilter = searchString;
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            IPagedList<OwnershipTypeDTO> ownershipTypes;
            if (!string.IsNullOrEmpty(searchString)){
                ownershipTypes = _manager
                    .GetAll(OwnershipTypeSpecification.FilterByName(searchString), d => d.Name)
                    .ToPagedList(pageNumber, pageSize);
            }
            else{
                ownershipTypes = _manager
                    .GetAll(d => d.Name)
                    .ToPagedList(pageNumber, pageSize);
            }
            return View(ownershipTypes);
        }
    }
}