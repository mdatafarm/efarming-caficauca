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
    /// Cooperatives Controller
    /// </summary>
    public class CooperativesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        ICooperativeManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="CooperativesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CooperativesController(CooperativeManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Cooperative</returns>
        [HttpGet]
        public ICollection<CooperativeDTO> Index()
        {
            return _manager.GetAll();
        }
    }
}
