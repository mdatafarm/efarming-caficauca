using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// ChechlistManager Interface
    /// </summary>
    public interface IChecklistManager
    {
        /// <summary>
        /// Gets all by farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>IEnumerable ChecklistDTO</returns>
        IEnumerable<ChecklistDTO> GetAllByFarm(Guid farmId);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ChecklistDTO</returns>
        ChecklistDTO Get(Guid id);

        /// <summary>
        /// Adds the specified checklist dto.
        /// </summary>
        /// <param name="checklistDTO">The checklist dto.</param>
        /// <returns>ChecklistDTO</returns>
        ChecklistDTO Add(ChecklistDTO checklistDTO);

        /// <summary>
        /// Edits the specified checklist dto.
        /// </summary>
        /// <param name="checklistDTO">The checklist dto.</param>
        /// <returns>bool</returns>
        bool Edit(ChecklistDTO checklistDTO);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>bool</returns>
        bool Delete(Guid id);
    }
}
