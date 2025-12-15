//using AutoMapper;
//using EFarming.Core.ImpactModule.IndicatorAggregate;
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
//    public class IndicatorsController : ApiController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IIndicatorManager _manager;
//        /// <summary>
//        /// Initializes a new instance of the <see cref="IndicatorsController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public IndicatorsController(IndicatorManager manager)
//        {
//            _manager = manager;
//        }

//        /// <summary>
//        /// Indexes the specified template identifier.
//        /// </summary>
//        /// <param name="templateId">The template identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public IEnumerable<IndicatorDTO> Index(Guid? templateId)
//        {
//            if (!templateId.HasValue)
//            {
//                return Mapper.Map<IEnumerable<IndicatorDTO>>(_manager.GetAll());
//            }
//            return Mapper.Map<IEnumerable<IndicatorDTO>>(_manager.GetAllByTemplate(templateId.Value));
//        }

//        /// <summary>
//        /// Creates the specified indicator.
//        /// </summary>
//        /// <param name="indicator">The indicator.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public IEnumerable<IndicatorDTO> Create(IndicatorDTO indicator)
//        {
//            _manager.Create(indicator);
//            indicator = _manager.Get(indicator.Id);
//            return Mapper.Map<IEnumerable<IndicatorDTO>>(_manager.GetAllByTemplate(indicator.AssessmentTemplateId));
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="indicator">The indicator.</param>
//        /// <returns></returns>
//        [HttpPut]
//        public IEnumerable<IndicatorDTO> Edit(Guid id, IndicatorDTO indicator)
//        {
//            _manager.Edit(id, indicator);
//            return Mapper.Map<IEnumerable<IndicatorDTO>>(_manager.GetAllByTemplate(indicator.AssessmentTemplateId));
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        [HttpDelete]
//        public IEnumerable<IndicatorDTO> Delete(Guid id)
//        {
//            var indicator = _manager.Get(id);
//            _manager.remove(id);
//            return Mapper.Map<IEnumerable<IndicatorDTO>>(_manager.GetAllByTemplate(indicator.AssessmentTemplateId));
//        }
//    }
//}
