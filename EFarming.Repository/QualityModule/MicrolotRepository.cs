using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.DAL;

namespace EFarming.Repository.QualityModule
{
    /// <summary>
    /// Microlot Repository
    /// </summary>
    public class MicrolotRepository : Repository<Microlot>, IMicrolotRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MicrolotRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public MicrolotRepository(UnitOfWork uow) : base(uow) { }
    }
}
