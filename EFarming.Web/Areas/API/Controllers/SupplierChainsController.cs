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
    /// 
    /// </summary>
    public class SupplierChainsController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private ISupplyChainManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierChainsController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public SupplierChainsController(SupplyChainManager manager){
            _manager = manager;
        }

        /// <summary>
        /// Listo of Cluster
        /// </summary>
        /// <param name="id">id </param>
        /// <returns>
        /// List of Cluster
        /// </returns>
        [HttpGet]
        public List<SupplyChainDTO> Index(Guid id)
        {
            return _manager.GetAllBySupplier(id).ToList();
        }
    }
}
