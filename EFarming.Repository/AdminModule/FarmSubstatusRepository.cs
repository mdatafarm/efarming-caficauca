using EFarming.Core.AdminModule.FarmSubstatusAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// FarmSubstatus Repository
    /// </summary>
    public class FarmSubstatusRepository : Repository<FarmSubstatus>, IFarmSubstatusRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FarmSubstatusRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public FarmSubstatusRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
