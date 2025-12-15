//using AutoMapper;
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
//    /// CriteriaOption
//    /// </summary>
//    public class CriteriaOptionsController : ApiController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IIndicatorManager _manager;
//        /// <summary>
//        /// Initializes a new instance of the <see cref="CriteriaOptionsController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public CriteriaOptionsController(IndicatorManager manager)
//        {
//            _manager = manager;
//        }

//        /// <summary>
//        /// Indexes the specified indicator identifier.
//        /// </summary>
//        /// <param name="indicatorId">The indicator identifier.</param>
//        /// <param name="criteriaId">The criteria identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public IEnumerable<CriteriaOptionDTO> Index(Guid indicatorId, Guid criteriaId)
//        {
//            return Mapper.Map<IEnumerable<CriteriaOptionDTO>>(_manager.Get(indicatorId).Criteria.First(c => c.Id.Equals(criteriaId)).OrderedCriteriaOptions);
//        }

//        /// <summary>
//        /// Creates the specified criteria option.
//        /// </summary>
//        /// <param name="criteriaOption">The criteria option.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public IEnumerable<CriteriaOptionDTO> Create(CriteriaOptionDTO criteriaOption)
//        {
//            var indicator = _manager.Get(criteriaOption.IndicatorId);
//            indicator.Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId)).CriteriaOptions.Add(criteriaOption);
//            _manager.Edit(criteriaOption.IndicatorId, criteriaOption.CriteriaId, indicator);
//            return Mapper.Map<IEnumerable<CriteriaOptionDTO>>(
//                _manager.Get(criteriaOption.IndicatorId).Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId)).OrderedCriteriaOptions);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="criteriaOption">The criteria option.</param>
//        /// <returns></returns>
//        [HttpPut]
//        public IEnumerable<CriteriaOptionDTO> Edit(Guid id, CriteriaOptionDTO criteriaOption)
//        {
//            var indicator = _manager.Get(criteriaOption.IndicatorId);
//            var toRemove = indicator.Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId))
//                .CriteriaOptions.First(co => co.Id.Equals(id));
//            indicator.Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId))
//                .CriteriaOptions.Remove(toRemove);
//            indicator.Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId))
//                .CriteriaOptions.Add(criteriaOption);
//            _manager.Edit(criteriaOption.IndicatorId, criteriaOption.CriteriaId, indicator);
//            return Mapper.Map<IEnumerable<CriteriaOptionDTO>>(
//                _manager.Get(criteriaOption.IndicatorId).Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId)).OrderedCriteriaOptions);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="criteriaOption">The criteria option.</param>
//        /// <returns></returns>
//        [HttpDelete]
//        public IEnumerable<CriteriaOptionDTO> Delete(Guid id, CriteriaOptionDTO criteriaOption)
//        {
//            var indicador = _manager.Get(criteriaOption.IndicatorId);
//            var toRemove = indicador.Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId)).CriteriaOptions.First(co => co.Id.Equals(id));
//            indicador.Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId)).CriteriaOptions.Remove(toRemove);
//            _manager.Edit(criteriaOption.IndicatorId, criteriaOption.CriteriaId, indicador);
//            return Mapper.Map<IEnumerable<CriteriaOptionDTO>>(
//                _manager.Get(criteriaOption.IndicatorId).Criteria.First(c => c.Id.Equals(criteriaOption.CriteriaId)).OrderedCriteriaOptions);
//        }
//    }
//}
