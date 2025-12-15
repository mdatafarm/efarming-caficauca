using EFarming.DTO.ImpactModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// ImpactManager Interface
    /// </summary>
    public interface IImpactManager
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>List of ImpactAssessmentDTO</returns>
        List<ImpactAssessmentDTO> GetAll(Guid farmId);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>Listo of ImpactAssessmentDTO</returns>
        List<ImpactAssessmentDTO> GetAll(int year);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ImpactAssessmentDTO</returns>
        ImpactAssessmentDTO Get(Guid id);

        /// <summary>
        /// Adds the specified impact assessment.
        /// </summary>
        /// <param name="impactAssessment">The impact assessment.</param>
        /// <returns>ImpactAssessmentDTO</returns>
        ImpactAssessmentDTO Add(ImpactAssessmentDTO impactAssessment);

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="impactAssessment">The impact assessment.</param>
        void Edit(Guid id, ImpactAssessmentDTO impactAssessment);

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        bool Remove(Guid id);
    }
}
