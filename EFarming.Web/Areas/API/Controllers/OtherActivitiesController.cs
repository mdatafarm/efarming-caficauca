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
    public class OtherActivitiesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IOtherActivityManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="OtherActivitiesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public OtherActivitiesController(OtherActivityManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<OtherActivityDTO> Index()
        {
            return _manager.GetAll();
        }
    }
}
