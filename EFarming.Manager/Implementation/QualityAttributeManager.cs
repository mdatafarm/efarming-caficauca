using AutoMapper;
using EFarming.Common.Consts;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Repository.QualityModule;
using System;
using System.Linq;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// QualityAttribute Manager
    /// </summary>
    public class QualityAttributeManager : IQualityAttributeManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IQualityAttributeRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="QualityAttributeManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public QualityAttributeManager(QualityAttributeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// QualityAttributeDTO
        /// </returns>
        public QualityAttributeDTO Find(Guid id)
        {
            return Mapper.Map<QualityAttributeDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Gets the specified template identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>
        /// ICollection of QualityAttributeDTO
        /// </returns>
        public ICollection<QualityAttributeDTO> Get(Guid templateId)
        {
            return Mapper.Map<ICollection<QualityAttributeDTO>>(
                _repository.AllMatching(QualityAttributeSpecification.ByTemplate(templateId)));
        }

        /// <summary>
        /// Adds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public void Add(QualityAttributeDTO attribute)
        {
            var entity = Mapper.Map<QualityAttribute>(attribute);
            if (entity.TypeOf.Equals(QualityAttributeTypes.OPTIONS))
            {
                foreach (var option in entity.OptionAttributes)
                {
                    option.QualityAttributeId = entity.Id;
                }
            }
            _repository.Add(entity);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Edits the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public void Edit(QualityAttributeDTO attribute)
        {
            var entity = Mapper.Map<QualityAttribute>(attribute);
            var persisted = _repository.Get(attribute.Id);
            if (entity.TypeOf.Equals(QualityAttributeTypes.OPTIONS))
            {
                foreach (var option in entity.OptionAttributes)
                {
                    option.QualityAttributeId = entity.Id;
                }
            }
            _repository.Merge(persisted, entity);
            _repository.UpdateAttribute(entity, persisted);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Deletes the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public void Delete(QualityAttributeDTO attribute)
        {
            var entity = _repository.Get(attribute.Id);
            _repository.Remove(entity);
            _repository.RemoveAttribute(entity);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Clears the attributes.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        private void ClearAttributes(QualityAttributeDTO attribute)
        {
            if (attribute.TypeOf.Equals(QualityAttributeTypes.OPEN_TEXT))
            {
                attribute.RangeAttribute = null;
                attribute.OptionAttributes = new List<OptionAttributeDTO>();
            }
            else if (attribute.TypeOf.Equals(QualityAttributeTypes.RANGE))
            {
                attribute.OpenTextAttribute = null;
                attribute.OptionAttributes = new List<OptionAttributeDTO>();
            }
            else if (attribute.TypeOf.Equals(QualityAttributeTypes.OPTIONS))
            {
                attribute.OpenTextAttribute = null;
                attribute.RangeAttribute = null;
            }
            else
            {
                attribute.OpenTextAttribute = null;
                attribute.RangeAttribute = null;
                attribute.OptionAttributes = new List<OptionAttributeDTO>();
            }
        }

        /// <summary>
        /// Gets the quantitative attributes.
        /// </summary>
        /// <returns>
        /// ICollection of QualityAttributeDTO
        /// </returns>
        public ICollection<QualityAttributeDTO> GetQuantitativeAttributes()
        {
            return Mapper.Map<ICollection<QualityAttributeDTO>>(_repository.AllMatching(QualityAttributeSpecification.CuantitativeAttributes()));
        }
    }
}
