using AutoMapper;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.DTO.ImpactModule;
using EFarming.Manager.Contract;
using EFarming.Repository.ImpactModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Impact Manager
    /// </summary>
    public class ImpactManager  : IImpactManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IImpactAssessmentRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImpactManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ImpactManager(ImpactAssessmentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>
        /// List of ImpactAssessmentDTO
        /// </returns>
        public List<ImpactAssessmentDTO> GetAll(Guid farmId)
        {
            return Mapper.Map<List<ImpactAssessmentDTO>>(_repository.AllMatching(ImpactAssessmentSpecification.FilterByFarm(farmId)));
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// ImpactAssessmentDTO
        /// </returns>
        public ImpactAssessmentDTO Get(Guid id)
        {
            return Mapper.Map<ImpactAssessmentDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Adds the specified impact assessment.
        /// </summary>
        /// <param name="impactAssessment">The impact assessment.</param>
        /// <returns>
        /// ImpactAssessmentDTO
        /// </returns>
        public ImpactAssessmentDTO Add(ImpactAssessmentDTO impactAssessment)
        {
            var entity = Mapper.Map<ImpactAssessment>(impactAssessment);
            _repository.Add(entity);
            _repository.UnitOfWork.Commit();
            return Mapper.Map<ImpactAssessmentDTO>(entity);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="impactAssessment">The impact assessment.</param>
        public void Edit(Guid id, ImpactAssessmentDTO impactAssessment)
        {
            var entity = Mapper.Map<ImpactAssessment>(impactAssessment);
            var persisted = _repository.Get(id);
            _repository.Merge(persisted, entity);
            _repository.UpdateAnswers(entity, persisted);
            _repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool Remove(Guid id)
        {
            var entity = _repository.Get(id);
            _repository.Remove(entity);
            _repository.UnitOfWork.Commit();
            return true;
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>
        /// Listo of ImpactAssessmentDTO
        /// </returns>
        public List<ImpactAssessmentDTO> GetAll(int year)
        {
            return Mapper.Map<List<ImpactAssessmentDTO>>(_repository.AllMatching(ImpactAssessmentSpecification.FilterByYear(year)));
        }
    }
}
