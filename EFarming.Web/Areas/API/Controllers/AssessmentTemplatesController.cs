using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// AssessmentTemplates Controller
    /// </summary>
    public class AssessmentTemplatesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IAssessmentTemplateManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentTemplatesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public AssessmentTemplatesController(IAssessmentTemplateManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        [HttpGet]
        public ICollection<AssessmentTemplateDTO> Get(string type)
        {
            return _manager.GetAll(AssessmentTemplateSpecification.ByType(type), at => at.Name);
        }
    }
}
