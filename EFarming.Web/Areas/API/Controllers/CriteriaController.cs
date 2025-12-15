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
//    /// Criteria Controller API
//    /// </summary>
//    public class CriteriaController : ApiController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        IIndicatorManager _manager;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CriteriaController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public CriteriaController(IndicatorManager manager)
//        {
//            _manager = manager;
//        }

//        /// <summary>
//        /// Indexes the specified indicator identifier.
//        /// </summary>
//        /// <param name="indicatorId">The indicator identifier.</param>
//        /// <returns>CriteriaDTO</returns>
//        [HttpGet]
//        public IEnumerable<CriteriaDTO> Index(Guid indicatorId)
//        {
//            return Mapper.Map<IEnumerable<CriteriaDTO>>(_manager.Get(indicatorId).Criteria);
//        }

//        /// <summary>
//        /// Creates the specified criteria.
//        /// </summary>
//        /// <param name="criteria">The criteria.</param>
//        /// <returns>Criteria</returns>
//        [HttpPost]
//        public IEnumerable<CriteriaDTO> Create(CriteriaDTO criteria)
//        {
//            var indicador = _manager.Get(criteria.IndicatorId);
//            indicador.Criteria.Add(criteria);
//            _manager.Edit(criteria.IndicatorId, indicador);
//            return Mapper.Map<IEnumerable<CriteriaDTO>>(_manager.Get(criteria.IndicatorId).Criteria);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="criteria">The criteria.</param>
//        /// <returns>Criteria</returns>
//        [HttpPut]
//        public IEnumerable<CriteriaDTO> Edit(Guid id, CriteriaDTO criteria)
//        {
//            var indicador = _manager.Get(criteria.IndicatorId);
//            var toRemove = indicador.Criteria.First(c => c.Id.Equals(id));
//            indicador.Criteria.Remove(toRemove);
//            indicador.Criteria.Add(criteria);
//            _manager.Edit(criteria.IndicatorId, indicador);
//            return Mapper.Map<IEnumerable<CriteriaDTO>>(_manager.Get(criteria.IndicatorId).Criteria);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="criteria">The criteria.</param>
//        /// <returns>Criteria</returns>
//        [HttpDelete]
//        public IEnumerable<CriteriaDTO> Delete(Guid id, CriteriaDTO criteria)
//        {
//            Guid indicatorId = criteria.IndicatorId;
//            var indicador = _manager.Get(indicatorId);
//            var toRemove = indicador.Criteria.First(c => c.Id.Equals(id));
//            indicador.Criteria.Remove(toRemove);
//            _manager.Edit(indicatorId, indicador);
//            return Mapper.Map<IEnumerable<CriteriaDTO>>(_manager.Get(indicatorId).Criteria);
//        }
//    }
//}
