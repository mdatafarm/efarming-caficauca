using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// Village Manager
    /// </summary>
    public class VillageManager : AdminManager<VillageDTO, VillageRepository, Village>, IVillageManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IVillageRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="VillageManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public VillageManager(VillageRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
