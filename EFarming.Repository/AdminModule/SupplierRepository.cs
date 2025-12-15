using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Supplier Repository
    /// </summary>
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public SupplierRepository(UnitOfWork uow) : base(uow) { }
    }
}
