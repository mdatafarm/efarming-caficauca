using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.DTO.AdminModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface ISupplyChainManager : IAdminManager<SupplyChainDTO, SupplyChain>
    {
        /// <summary>
        /// Gets all by supplier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ICollection SupplyChainDTO</returns>
        ICollection<SupplyChainDTO> GetAllBySupplier(Guid id);
    }
}
