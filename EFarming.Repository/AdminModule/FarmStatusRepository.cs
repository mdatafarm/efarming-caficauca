using EFarming.Core.AdminModule.FarmStatusAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// FarmStatus Repository
    /// </summary>
    public class FarmStatusRepository : Repository<FarmStatus>, IFarmStatusRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmStatusRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public FarmStatusRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
