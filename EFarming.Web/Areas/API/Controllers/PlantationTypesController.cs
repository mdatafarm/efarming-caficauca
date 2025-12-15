using AutoMapper;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
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
    public class PlantationTypesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IPlantationTypeManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationTypesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PlantationTypesController(PlantationTypeManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<PlantationType> Index()
        {
            return Mapper.Map<ICollection<PlantationType>>(_manager.GetAll());
        }
    }
}
