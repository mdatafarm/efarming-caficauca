using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.DAL;

namespace EFarming.Repository.ImpactModule
{
    /// <summary>
    /// Requirement Repository
    /// </summary>
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequirementRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public RequirementRepository(UnitOfWork uow)
            : base(uow)
        { }
    }
}
