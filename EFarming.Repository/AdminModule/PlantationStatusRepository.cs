using EFarming.Core.AdminModule.PlantationStatusAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// PlantationStatus Repository
    /// </summary>
    public class PlantationStatusRepository : Repository<PlantationStatus>, IPlantationStatusRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlantationStatusRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PlantationStatusRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) { }
    }
}
