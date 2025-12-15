using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Village Repository
    /// </summary>
    public class VillageRepository: Repository<Village>, IVillageRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VillageRepository"/> class.
        /// </summary>
        /// <param name="unityOfWork">The unity of work.</param>
        public VillageRepository(UnitOfWork unityOfWork) : base(unityOfWork) { }
    }
}
