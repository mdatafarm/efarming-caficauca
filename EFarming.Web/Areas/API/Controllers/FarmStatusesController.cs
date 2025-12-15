using AutoMapper;
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
    /// FarmStatus Controller
    /// </summary>
    public class FarmStatusesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmStatusManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmStatusesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FarmStatusesController(FarmStatusManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// List of FarmStatus
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<FarmStatusDTO> Index()
        {
            return Mapper.Map<ICollection<FarmStatusDTO>>(_manager.GetAll());
        }
    }
}
