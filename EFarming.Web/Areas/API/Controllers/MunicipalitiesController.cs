using EFarming.Core.AdminModule.MunicipalityAggregate;
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
    public class MunicipalitiesController : ApiController
    {
        /// <summary>
        /// The _municipality manager
        /// </summary>
        private IMunicipalityManager _municipalityManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="MunicipalitiesController"/> class.
        /// </summary>
        /// <param name="municipalityManager">The municipality manager.</param>
        public MunicipalitiesController(MunicipalityManager municipalityManager)
        {
            _municipalityManager = municipalityManager;
        }

        /// <summary>
        /// Indexes the specified department identifier.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public List<MunicipalityDTO> Index(Guid departmentId)
        {
            var m = _municipalityManager
                .GetAll(MunicipalitySpecification.FilterMunicipalities(departmentId, string.Empty), d => d.Name);
            return m.ToList();
        }
    }
}
