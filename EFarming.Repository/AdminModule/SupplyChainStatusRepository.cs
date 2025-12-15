using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// SupplyChainStatus Repository
    /// </summary>
    public class SupplyChainStatusRepository : Repository<SupplyChainStatus>, ISupplyChainStatusRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplyChainStatusRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public SupplyChainStatusRepository(UnitOfWork uow) : base(uow) { }
    }
}
