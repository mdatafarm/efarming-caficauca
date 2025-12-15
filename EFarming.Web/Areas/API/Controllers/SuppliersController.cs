using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Suppliers Controller
    /// </summary>
    public class SuppliersController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private ISupplierManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="SuppliersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public SuppliersController(SupplierManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Cluster List
        /// </summary>
        /// <param name="id">country di</param>
        /// <returns>
        /// Cluster List
        /// </returns>
        [HttpGet]        
        public List<SupplierDTO> Index(Guid id)
        {
            return _manager.GetAll(SupplierSpecification.Filter(string.Empty, id), s => s.Name).ToList();
        }

    }
}
