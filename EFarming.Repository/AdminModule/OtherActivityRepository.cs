using EFarming.Core.AdminModule.OtherActivitiesAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// OtherActivity Repository
    /// </summary>
    public class OtherActivityRepository : Repository<OtherActivity>, IOtherActivityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OtherActivityRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public OtherActivityRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) { }
    }
}
