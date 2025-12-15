using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.DAL;
using System.Linq;

namespace EFarming.Repository.ImpactModule
{
    /// <summary>
    /// Category Repository
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public CategoryRepository(UnitOfWork uow)
            : base(uow) { }

        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <param name="includes"></param>
        /// <returns>
        /// List of selected elements
        /// </returns>
        public override IQueryable<Category> GetAll(params string[] includes)
        {
            var data = base.GetAll(includes);
            return data.OrderByDescending(d => d.Name);
        }
    }
}
