//using AutoMapper;
//using EFarming.Core.QualityModule.SensoryProfileAggregate;
//using EFarming.DTO.QualityModule;
//using EFarming.Manager.Contract;
//using EFarming.Manager.Implementation;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace EFarming.Web.Areas.API.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class SensoryProfileAssessmentsController : ApiController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private ISensoryProfileManager _manager;
//        /// <summary>
//        /// The _quality manager
//        /// </summary>
//        private IQualityAttributeManager _qualityManager;
//        /// <summary>
//        /// Initializes a new instance of the <see cref="SensoryProfileAssessmentsController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        /// <param name="qualityManager">The quality manager.</param>
//        public SensoryProfileAssessmentsController(
//            SensoryProfileManager manager,
//            QualityAttributeManager qualityManager)
//        {
//            _manager = manager;
//            _qualityManager = qualityManager;
//        }

//        /// <summary>
//        /// Indexes the specified farm identifier.
//        /// </summary>
//        /// <param name="farmId">The farm identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public ICollection<SensoryProfileAssessmentDTOAPI> Index(Guid farmId)
//        {
//            var c = Mapper.Map<ICollection<SensoryProfileAssessmentDTOAPI>>(_manager.GetAll(farmId));
//            return c;
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public object Details(Guid id)
//        {
//            var series = new List<object>();
//            var data = new List<double>();
//            var qualityAttributes = _qualityManager.GetQuantitativeAttributes();

//            var assessment = _manager.Get(id);

//            foreach (var qualityAttribute in qualityAttributes)
//            {
//                var ans = assessment.SensoryProfileAnswers.FirstOrDefault(spa => spa.QualityAttributeId.Equals(qualityAttribute.Id));
//                if (ans != null)
//                    data.Add(Double.Parse(ans.Answer, CultureInfo.GetCultureInfo("en")));
//            }
//            series.Add(new { name = assessment.Description, data = data, pointPlacement = "on" });
//            return new { categories = qualityAttributes.Select(qa => qa.Description), series = series, title = assessment.Description };
//        }

//        /// <summary>
//        /// Creates the specified assessment.
//        /// </summary>
//        /// <param name="assessment">The assessment.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public SensoryProfileAssessmentDTO Create(SensoryProfileAssessmentDTO assessment)
//        {
//            assessment.Type = SensoryProfileAssessment.FARM;
//            return _manager.Add(assessment);
//        }
//    }
//}
