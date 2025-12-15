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
    public class PlantationStatusesController : ApiController
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
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<PlantationStatusDTO> Index()
        {
            return _manager.GetAll();
        }
    }
}
