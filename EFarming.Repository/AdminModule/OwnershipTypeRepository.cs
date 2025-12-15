using EFarming.Core.AdminModule.OwnershipTypeAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// OwnershipType Repository
    /// </summary>
    public class OwnershipTypeRepository : Repository<OwnershipType>, IOwnershipTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnershipTypeRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public OwnershipTypeRepository(UnitOfWork unitOfWork)
            : base(unitOfWork) { }
    }
}
