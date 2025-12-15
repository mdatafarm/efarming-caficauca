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
    public class OwnershipTypesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IOwnershipTypeManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnershipTypesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public OwnershipTypesController(OwnershipTypeManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<OwnershipTypeDTO> Index()
        {
            return _manager.GetAll();
        }
    }
}
