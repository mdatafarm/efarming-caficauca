using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// SensoryProfileManager Interface
    /// </summary>
    public interface ISensoryProfileManager
    {
        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <returns>List</returns>
        List<string> GetTypes();

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>SensoryProfileAssessmentDTO</returns>
        SensoryProfileAssessmentDTO Get(Guid id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ICollection SensoryProfileAssessmentDTO</returns>
        ICollection<SensoryProfileAssessmentDTO> GetAll(Guid id);

        /// <summary>
        /// Adds the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>SensoryProfileAssessmentDTO</returns>
        SensoryProfileAssessmentDTO Add(SensoryProfileAssessmentDTO entityDTO);

        /// <summary>
        /// Edits the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>SensoryProfileAssessmentDTO</returns>
        SensoryProfileAssessmentDTO Edit(SensoryProfileAssessmentDTO entityDTO);

        /// <summary>
        /// Removes the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>ICollection SensoryProfileAssessmentDTO</returns>
        ICollection<SensoryProfileAssessmentDTO> Remove(SensoryProfileAssessmentDTO entityDTO);

        /// <summary>
        /// Filters the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="description">The description.</param>
        /// <returns>ICollection SensoryProfileAssessmentDTO</returns>
        ICollection<SensoryProfileAssessmentDTO> Filter(Guid? userId, string description);

        IQueryable<SensoryProfileAssessment> GetAllQueryable<KProperty>(Specification<SensoryProfileAssessment> filterSpecification, Expression<Func<SensoryProfileAssessment, KProperty>> orderByExpression);
        /* ICollection<SensoryProfileAssessmentDTO> GetAllQueryable<KProperty>(Specification<SensoryProfileAssessment> fi
        lterSpecification, Expression<Func<SensoryProfileAssessment, KProperty>> orderByExpression);*/

        ICollection<SensoryProfileAssessmentDTO> FilterByAnything(  Guid templateId, Guid? searchVillage, Guid? searchMunicipality, Guid? searchDepartment, string searchFarm, Guid? searchTaster);

        //ICollection<SensoryProfileAssessment> FilterById(Guid Id);
        ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> FilterById(Guid Id);
        /// <summary>
        /// Filters the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="tasterId">The taster identifier.</param>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>ICollection SensoryProfileAssessmentDTO</returns>
        ICollection<SensoryProfileAssessmentDTO> Filter(DateTime? start, DateTime? end, Guid? tasterId, Guid templateId);

        /// <summary>
        /// Filters by FarmID
        /// </summary>
        /// <param name="id">FarmID</param>
        /// <param name="templateId">Template to filter</param>     
        /// <returns></returns>
        ICollection<SensoryProfileAssessmentDTO> FilterByFarmAndTemplate(Guid id, Guid templateId);

        /// <summary>
        /// Destroys the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Destroy(Guid id);
    }
}
