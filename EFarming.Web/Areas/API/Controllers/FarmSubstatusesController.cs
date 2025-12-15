using EFarming.Core.AdminModule.FarmSubstatusAggregate;
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
    public class FarmSubstatusesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmSubstatusManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmSubstatusesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FarmSubstatusesController(FarmSubstatusManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified farm status identifier.
        /// </summary>
        /// <param name="farmStatusId">The farm status identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ICollection<FarmSubstatusDTO> Index(Guid farmStatusId)
        {
            return _manager.GetAll(FarmSubstatusSpecification.FilterFarmSubstatus(string.Empty, farmStatusId), fss => fss.Name);
        }
    }
}
