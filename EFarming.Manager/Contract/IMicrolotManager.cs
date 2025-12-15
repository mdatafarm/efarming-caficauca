using EFarming.DTO.QualityModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// MicrolotManager Interface
    /// </summary>
    public interface IMicrolotManager
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable MicrolotDTO</returns>
        IEnumerable<MicrolotDTO> GetAll();

        /// <summary>
        /// Bies the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>MicrolotDTO</returns>
        MicrolotDTO ByCode(string code);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>MicrolotDTO</returns>
        MicrolotDTO Get(Guid id);

        /// <summary>
        /// Adds the specified microlot dto.
        /// </summary>
        /// <param name="microlotDTO">The microlot dto.</param>
        void Add(MicrolotDTO microlotDTO);

        /// <summary>
        /// Edits the specified microlot dto.
        /// </summary>
        /// <param name="microlotDTO">The microlot dto.</param>
        void Edit(MicrolotDTO microlotDTO);

        /// <summary>
        /// Removes the specified microlot dto.
        /// </summary>
        /// <param name="microlotDTO">The microlot dto.</param>
        void Remove(MicrolotDTO microlotDTO);
    }
}
