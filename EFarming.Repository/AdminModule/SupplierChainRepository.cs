using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// SupplyChain Repository
    /// </summary>
    public class SupplyChainRepository : Repository<SupplyChain>, ISupplyChainRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyChainRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public SupplyChainRepository(UnitOfWork uow) : base(uow) { }
    }
}
