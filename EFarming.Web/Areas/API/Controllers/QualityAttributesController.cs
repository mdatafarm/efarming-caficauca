//using EFarming.DTO.QualityModule;
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
//    public class QualityAttributesController : ApiController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IQualityAttributeManager _manager;
//        /// <summary>
//        /// Initializes a new instance of the <see cref="QualityAttributesController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public QualityAttributesController(QualityAttributeManager manager)
//        {
//            _manager = manager;
//        }

//        /// <summary>
//        /// Indexes the specified template identifier.
//        /// </summary>
//        /// <param name="templateId">The template identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public ICollection<QualityAttributeDTO> Index(Guid templateId)
//        {
//            return _manager.Get(templateId);
//        }

//        /// <summary>
//        /// Creates the specified attribute.
//        /// </summary>
//        /// <param name="attribute">The attribute.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public ICollection<QualityAttributeDTO> Create(QualityAttributeDTO attribute)
//        {
//            _manager.Add(attribute);
//            return _manager.Get(attribute.AssessmentTemplateId);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="attribute">The attribute.</param>
//        /// <returns></returns>
//        [HttpPut]
//        public ICollection<QualityAttributeDTO> Edit(Guid id, QualityAttributeDTO attribute)
//        {
//            _manager.Edit(attribute);
//            return _manager.Get(attribute.AssessmentTemplateId);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="attribute">The attribute.</param>
//        /// <returns></returns>
//        [HttpDelete]
//        public ICollection<QualityAttributeDTO> Delete(Guid id, QualityAttributeDTO attribute)
//        {
//            _manager.Delete(attribute);
//            return _manager.Get(attribute.AssessmentTemplateId);
//        }
//    }
//}
