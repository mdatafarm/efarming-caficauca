using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.ImpactModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    [RoutePrefix("sustainability")]
    public class SustainabilityController : ApiController
    {
        private IAssessmentTemplateManager _assessmentTemplateManager;
        private IImpactManager _impactManager;
        public SustainabilityController(
            IAssessmentTemplateManager assessmentTemplateManager,
            IImpactManager impactManager)
        {
            _assessmentTemplateManager = assessmentTemplateManager;
            _impactManager = impactManager;
        }

        [HttpGet]
        [Route("")]
        public ICollection<AssessmentTemplateDTO> Index()
        {
            return _assessmentTemplateManager
                .GetAll(AssessmentTemplateSpecification.Sustainability()
                        , at => at.Name,
                        "Categories",
                        "Categories.Indicators",
                        "Categories.Indicators.Criteria",
                        "Categories.Indicators.Criteria.CriteriaOptions");
        }
    }
}
