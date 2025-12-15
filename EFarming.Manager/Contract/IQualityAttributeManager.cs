using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// QualityAttributeManager Interface
    /// </summary>
    public interface IQualityAttributeManager
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>QualityAttributeDTO</returns>
        QualityAttributeDTO Find(Guid id);

        /// <summary>
        /// Gets the specified template identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>ICollection of QualityAttributeDTO</returns>
        ICollection<QualityAttributeDTO> Get(Guid templateId);

        /// <summary>
        /// Adds the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        void Add(QualityAttributeDTO attribute);

        /// <summary>
        /// Edits the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        void Edit(QualityAttributeDTO attribute);

        /// <summary>
        /// Deletes the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        void Delete(QualityAttributeDTO attribute);

        /// <summary>
        /// Gets the quantitative attributes.
        /// </summary>
        /// <returns>ICollection of QualityAttributeDTO</returns>
        ICollection<QualityAttributeDTO> GetQuantitativeAttributes();
    }
}
