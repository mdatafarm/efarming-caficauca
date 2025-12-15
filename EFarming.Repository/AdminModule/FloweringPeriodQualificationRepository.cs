using EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// FloweringPeriodQualification Repository
    /// </summary>
    public class FloweringPeriodQualificationRepository : Repository<FloweringPeriodQualification>, IFloweringPeriodQualificationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FloweringPeriodQualificationRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public FloweringPeriodQualificationRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
