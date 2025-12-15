using EFarming.Core.AdminModule.OwnershipTypeAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// OwnerShipType Manager
    /// </summary>
    public class OwnershipTypeManager : AdminManager<OwnershipTypeDTO, OwnershipTypeRepository, OwnershipType>, IOwnershipTypeManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IOwnershipTypeRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnershipTypeManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public OwnershipTypeManager(OwnershipTypeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
