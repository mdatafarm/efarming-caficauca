using EFarming.Core.QualityModule.QualityProfileAggregate;
using EFarming.DAL;

namespace EFarming.Repository.QualityModule
{
    /// <summary>
    /// QualityPrifile Repository
    /// </summary>
    public class QualityProfileRepository : Repository<QualityProfile>, IQualityProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QualityProfileRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public QualityProfileRepository(UnitOfWork uow) : base(uow) { }
    }
}
