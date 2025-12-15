using EFarming.DTO.TraceabilityModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// LotManager Interface
    /// </summary>
    public interface ILotManager
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable LotDTO</returns>
        IEnumerable<LotDTO> GetAll();

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>LotDTO</returns>
        LotDTO Get(Guid id);

        /// <summary>
        /// Adds the specified lot dto.
        /// </summary>
        /// <param name="lotDTO">The lot dto.</param>
        void Add(LotDTO lotDTO);

        /// <summary>
        /// Edits the specified lot dto.
        /// </summary>
        /// <param name="lotDTO">The lot dto.</param>
        void Edit(LotDTO lotDTO);

        /// <summary>
        /// Removes the specified lot dto.
        /// </summary>
        /// <param name="lotDTO">The lot dto.</param>
        void Remove(LotDTO lotDTO);
    }
}
