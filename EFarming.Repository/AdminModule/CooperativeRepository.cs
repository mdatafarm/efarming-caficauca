using EFarming.Core.AdminModule.CooperativeAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Cooperative Repository
    /// </summary>
    public class CooperativeRepository : Repository<Cooperative>, ICooperativeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CooperativeRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CooperativeRepository(UnitOfWork unitOfWork): base(unitOfWork) { }
    }
}
