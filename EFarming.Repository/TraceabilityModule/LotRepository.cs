using EFarming.Core.TraceabilityModule.LotAggregate;
using EFarming.DAL;

namespace EFarming.Repository.TraceabilityModule
{
    /// <summary>
    /// Lot Repository
    /// </summary>
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LotRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public LotRepository(UnitOfWork uow) : base(uow) { }
    }
}
