using AutoMapper;
using EFarming.Core.AdminModule.VillageAggregate;
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
    public class VillagesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IVillageManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="VillagesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public VillagesController(VillageManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified municipality identifier.
        /// </summary>
        /// <param name="municipalityId">The municipality identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ICollection<VillageDTO> Index(Guid municipalityId)
        {
            return Mapper.Map<ICollection<VillageDTO>>(_manager.GetAll(VillageSpecification.FilterVillages(string.Empty, municipalityId, Guid.Empty), v => v.Name));
        }
    }
}
