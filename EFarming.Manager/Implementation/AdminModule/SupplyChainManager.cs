using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// SupplyChain Manager
    /// </summary>
    public class SupplyChainManager : AdminManager<SupplyChainDTO, SupplyChainRepository, SupplyChain>, ISupplyChainManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ISupplyChainRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyChainManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SupplyChainManager(SupplyChainRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all by supplier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// ICollection SupplyChainDTO
        /// </returns>
        public ICollection<SupplyChainDTO> GetAllBySupplier(Guid id)
        {
            return base.GetAll(SupplyChainSpecification.Filter(string.Empty, id), sc => sc.Name);
        }
    }
}
