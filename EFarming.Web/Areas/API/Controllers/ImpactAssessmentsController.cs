//using EFarming.DTO.ImpactModule;
//using EFarming.Manager.Contract;
//using EFarming.Manager.Implementation;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace EFarming.Web.Areas.API.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class ImpactAssessmentsController : ApiController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IImpactManager _manager;
//        /// <summary>
//        /// Initializes a new instance of the <see cref="ImpactAssessmentsController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public ImpactAssessmentsController(ImpactManager manager)
//        {
//            _manager = manager;
//        }

//        /// <summary>
//        /// Indexes the specified farm identifier.
//        /// </summary>
//        /// <param name="farmId">The farm identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public ICollection<ImpactAssessmentDTO> Index(Guid farmId)
//        {
//            return _manager.GetAll(farmId);
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public ImpactAssessmentDTO Details(Guid id)
//        {
//            return _manager.Get(id);
//        }

//        /// <summary>
//        /// Creates the specified impact assessment.
//        /// </summary>
//        /// <param name="impactAssessment">The impact assessment.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public ImpactAssessmentDTO Create(ImpactAssessmentDTO impactAssessment)
//        {
//            _manager.Add(impactAssessment);
//            return _manager.Get(impactAssessment.Id);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="impactAssessment">The impact assessment.</param>
//        /// <returns></returns>
//        [HttpPut]
//        public ImpactAssessmentDTO Edit(Guid id, ImpactAssessmentDTO impactAssessment){
//            _manager.Edit(id, impactAssessment);
//            return _manager.Get(impactAssessment.Id);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="impactAssessment">The impact assessment.</param>
//        /// <returns></returns>
//        [HttpDelete]
//        public ICollection<ImpactAssessmentDTO> Delete(Guid id, ImpactAssessmentDTO impactAssessment)
//        {
//            _manager.Remove(id);
//            return _manager.GetAll(impactAssessment.FarmId);
//        }
//    }
//}
