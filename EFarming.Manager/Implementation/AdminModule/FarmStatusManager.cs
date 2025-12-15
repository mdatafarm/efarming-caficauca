using EFarming.Core.AdminModule.FarmStatusAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// FarmStatus Manager
    /// </summary>
    public class FarmStatusManager : AdminManager<FarmStatusDTO, FarmStatusRepository, FarmStatus>, IFarmStatusManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IFarmStatusRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmStatusManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public FarmStatusManager(FarmStatusRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
