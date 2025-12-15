using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.DAL;
using System.Linq;

namespace EFarming.Repository.ImpactModule
{
    /// <summary>
    /// Criteria Repository
    /// </summary>
    public class CriteriaRepository : Repository<Criteria>, ICriteriaRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public CriteriaRepository(UnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public override IQueryable<Criteria> GetAll(params string[] includes)
        {
            var data = base.GetAll(includes);
            return data.OrderByDescending(d => d.Value);
        }
    }
}
