using AutoMapper;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.DTO.FarmModule;
using EFarming.DTO.QualityModule;
using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using EFarming.Repository.QualityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// SensoryProfile Manager
    /// </summary>
    public class SensoryProfileManager : ISensoryProfileManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ISensoryProfileRepository _repository;
        /// <summary>
        /// The _farm mananager
        /// </summary>
        private IFarmManager _farmMananager;
        /// <summary>
        /// The _microlot manager
        /// </summary>
        private IMicrolotManager _microlotManager;
        /// <summary>
        /// The _invoice manager
        /// </summary>
        private IInvoiceManager _invoiceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensoryProfileManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="farmRepository">The farm repository.</param>
        /// <param name="microlotRepository">The microlot repository.</param>
        /// <param name="invoiceRepository">The invoice repository.</param>
        public SensoryProfileManager(
            SensoryProfileRepository repository,
            IFarmManager farmRepository,
            IMicrolotManager microlotRepository,
            IInvoiceManager invoiceRepository)
        {
            _repository = repository;
            _farmMananager = farmRepository;
            _microlotManager = microlotRepository;
            _invoiceManager = invoiceRepository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// ICollection SensoryProfileAssessmentDTO
        /// </returns>
        public ICollection<SensoryProfileAssessmentDTO> GetAll(Guid id)
        {
            var c = _repository.AllMatching(SensoryProfileAssessmentSpecification.FilterByFarm(id));
            return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(c);
        }

        /// <summary>
        /// Adds the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>
        /// SensoryProfileAssessmentDTO
        /// </returns>
        public SensoryProfileAssessmentDTO Add(SensoryProfileAssessmentDTO entityDTO)
        {
            var entity = Mapper.Map<SensoryProfileAssessment>(entityDTO);
            if (entityDTO.Type.Equals(SensoryProfileAssessment.FARM))
            {
                FarmDTO farm;
                if (entityDTO.FarmId.HasValue && Guid.Empty != entityDTO.FarmId.Value)                
                    farm = _farmMananager.Details(entity.FarmId.Value);
                else
                    farm = _farmMananager.GetAll(FarmSpecification.Filter(entityDTO.Code, string.Empty, null, null, null, null, null, null), f => f.Code).FirstOrDefault();
                if (farm != null)
                {
                    entity.Type = SensoryProfileAssessment.FARM;
                    entity.FarmId = farm.Id;
                }
            }
            else if (entityDTO.Type.Equals(SensoryProfileAssessment.MICROLOT))
            {
                var microlot = _microlotManager.ByCode(entityDTO.Code);
                if (microlot == null)
                {
                    _microlotManager.Add(new MicrolotDTO { Code = entityDTO.Code });
                    microlot = _microlotManager.ByCode(entityDTO.Code);
                }
                entity.Type = SensoryProfileAssessment.MICROLOT;
                entity.MicrolotId = microlot.Id;
            }
            else if (entityDTO.Type.Equals(SensoryProfileAssessment.TRANSACTION))
            {
                var invoice = _invoiceManager.ByReceipt(Int32.Parse(entityDTO.Code));
                if (invoice == null)
                {
                    _invoiceManager.Add(new InvoiceDTO
                    {
                        Date = DateTime.UtcNow,
                        Identification = string.Empty,
                        Value = 0,
                        InvoiceNumber = Int32.Parse(entityDTO.Code),
                        Weight = 0,
                    });
                    invoice = _invoiceManager.ByReceipt(Int32.Parse(entityDTO.Code));
                }
                entity.Type = SensoryProfileAssessment.TRANSACTION;
                entity.InvoiceId = invoice.Id;
            }
            _repository.Add(entity);
            _repository.UnitOfWork.Commit();
            return Mapper.Map<SensoryProfileAssessmentDTO>(entity);
        }

        /// <summary>
        /// Edits the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>
        /// SensoryProfileAssessmentDTO
        /// </returns>
        public SensoryProfileAssessmentDTO Edit(SensoryProfileAssessmentDTO entityDTO)
        {
            var entity = Mapper.Map<SensoryProfileAssessment>(entityDTO);
            var persisted = _repository.Get(entity.Id);
            _repository.Merge(persisted, entity);
            _repository.UpdateAnswers(entity, persisted);
            _repository.UnitOfWork.Commit();
            return Mapper.Map<SensoryProfileAssessmentDTO>(entity);
        }

        /// <summary>
        /// Removes the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>
        /// ICollection SensoryProfileAssessmentDTO
        /// </returns>
        public ICollection<SensoryProfileAssessmentDTO> Remove(SensoryProfileAssessmentDTO entityDTO)
        {
            var entity = _repository.Get(entityDTO.Id);
            _repository.Remove(entity);
            _repository.UnitOfWork.Commit();
            return GetAll(entityDTO.FarmId.Value);
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// SensoryProfileAssessmentDTO
        /// </returns>
        public SensoryProfileAssessmentDTO Get(Guid id)
        {
            return Mapper.Map<SensoryProfileAssessmentDTO>(_repository.Get(id));
        }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <returns>
        /// List
        /// </returns>
        public List<string> GetTypes()
        {
            return SensoryProfileAssessment.TYPES;
        }

        /// <summary>
        /// Filters the specified user identifier.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public ICollection<SensoryProfileAssessmentDTO> Filter(Guid? userID, string description)
        {
            var assessment = _repository
                .AllMatching(SensoryProfileAssessmentSpecification.FilterByUserAndDescription(description, userID));
            return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(assessment);
        }

        /// <summary>
        /// Filters the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="tasterId">The taster identifier.</param>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>
        /// ICollection SensoryProfileAssessmentDTO
        /// </returns>
        public ICollection<SensoryProfileAssessmentDTO> Filter(DateTime? start, DateTime? end, Guid? tasterId, Guid templateId)
        {
            var res = _repository
                .AllMatching(SensoryProfileAssessmentSpecification.FilterByRangeAndTaster(start, end, tasterId, templateId))
                .OrderByDescending(spa => spa.Date);
           
            return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(res);
        }

        public ICollection<SensoryProfileAssessmentDTO> FilterByFarmAndTemplate(Guid id, Guid templateId)
        {
            var res = _repository
                .AllMatching(SensoryProfileAssessmentSpecification.FilterByFarmAndTemplate(id, templateId))
                .OrderByDescending(spa => spa.Date);

            return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(res);
        }

        /// <summary>
        /// Destroys the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Destroy(Guid id)
        {
            var assessment = _repository.Get(id);
            _repository.Destroy(assessment);
            _repository.UnitOfWork.Commit();
        }

        public IQueryable<SensoryProfileAssessment> GetAllQueryable<KProperty>(Specification<SensoryProfileAssessment> filterSpecification, Expression<Func<SensoryProfileAssessment, KProperty>> orderByExpression)
        {
            var result = _repository.AllMatching(filterSpecification)
                .OrderBy(orderByExpression);

            return result;
        }
        //public ICollection<SensoryProfileAssessmentDTO> GetAllQueryable<KProperty>(Specification<SensoryProfileAssessment> filterSpecification, Expression<Func<SensoryProfileAssessment, KProperty>> orderByExpression)
        //{
        //    var result = _repository.AllMatching(filterSpecification)
        //        .OrderBy(orderByExpression);

        //    return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(result);
        //}

        public ICollection<SensoryProfileAssessmentDTO> FilterByAnything(Guid templateId, Guid? searchVillage, Guid? searchMunicipality, Guid? searchDepartment, string searchFarm, Guid? searchTaster)
        {
            var res = _repository
                .AllMatching(SensoryProfileAssessmentSpecification.FilterByAnything(templateId, searchVillage, searchMunicipality, searchDepartment, searchFarm, searchTaster));

            return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(res);
        }
        public ICollection<SensoryProfileAssessmentDTO> FilterById(Guid Id)
        {
            var res = _repository
                .AllMatching(SensoryProfileAssessmentSpecification.FilterById(Id));
            return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(res);
            //return Mapper.Map<ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO>>(res);

        }

        /* ICollection<SensoryProfileAssessmentDTO> ISensoryProfileManager FilterByAnything(Guid templateId, Guid? searchVillage, Guid? searchMunicipality, Guid? searchDepartment, Guid? searchTaster)
         {
             var res = _repository
                  .AllMatching(SensoryProfileAssessmentSpecification.FilterByAnything(templateId, searchVillage, searchMunicipality, searchDepartment, searchTaster));

             return Mapper.Map<ICollection<SensoryProfileAssessmentDTO>>(res);
         }*/
    }
}
