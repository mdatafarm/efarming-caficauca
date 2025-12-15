using EFarming.Core.AdminModule.PlantationStatusAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// PlantationStatus Manager
    /// </summary>
    public class PlantationStatusManager : AdminManager<PlantationStatusDTO, PlantationStatusRepository, PlantationStatus>, IPlantationStatusManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IPlantationStatusRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationStatusManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public PlantationStatusManager(PlantationStatusRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
