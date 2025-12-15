using EFarming.Core.AdminModule.PlantationVarietyAggregate;
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
    public class PlantationVarietiesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IPlantationVarietyManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationVarietiesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PlantationVarietiesController(PlantationVarietyManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified plantation type identifier.
        /// </summary>
        /// <param name="plantationTypeId">The plantation type identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PlantationVarietyDTO> Index(Guid plantationTypeId)
        {
            var varieties = _manager.GetAll(
                PlantationVarietySpecification.FilterPlantationVariety(string.Empty, plantationTypeId),
                pv => pv.Name);
            return varieties;
        }
    }
}
