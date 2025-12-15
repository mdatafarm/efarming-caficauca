using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.DAL;
using System.Linq;

namespace EFarming.Repository.ImpactModule
{
    /// <summary>
    /// Indicator Repository
    /// </summary>
    public class IndicatorRepository : Repository<Indicator>, IIndicatorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndicatorRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public IndicatorRepository(UnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public override IQueryable<Indicator> GetAll(params string[] includes)
        {
            var data = base.GetAll(includes);
            return data.OrderBy(d => d.Name);
        }
    }
}
