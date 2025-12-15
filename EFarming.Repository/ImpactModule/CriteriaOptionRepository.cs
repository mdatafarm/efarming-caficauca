using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.DAL;
using System.Linq;

namespace EFarming.Repository.ImpactModule
{
    /// <summary>
    /// CriteriaOption Repository
    /// </summary>
    public class CriteriaOptionRepository : Repository<CriteriaOption>, ICriteriaOptionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaOptionRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public CriteriaOptionRepository(UnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public override IQueryable<CriteriaOption> GetAll(params string[] includes)
        {
            var data = base.GetAll(includes);
            return data.OrderByDescending(d => d.Value);
        }
    }
}
