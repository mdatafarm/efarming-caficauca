using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.DAL;

namespace EFarming.Repository.FarmModule
{
    /// <summary>
    /// FamilyUnit Repository
    /// </summary>
    public class FamilyUnitRepository : Repository<FamilyUnitMember>, IFamilyUnitRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyUnitRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public FamilyUnitRepository(UnitOfWork uow) : base(uow) { }
    }
}
