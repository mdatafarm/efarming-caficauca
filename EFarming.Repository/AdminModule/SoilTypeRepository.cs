using EFarming.Core.AdminModule.SoilTypeAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// SoilType Repository
    /// </summary>
    public class SoilTypeRepository : Repository<SoilType>, ISoilTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoilTypeRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public SoilTypeRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
