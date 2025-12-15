using EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// FlowerinPeriodQualification Manager
    /// </summary>
    public class FloweringPeriodQualificationManager : AdminManager<FloweringPeriodQualificationDTO, FloweringPeriodQualificationRepository, FloweringPeriodQualification>, IFloweringPeriodQualificationManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IFloweringPeriodQualificationRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloweringPeriodQualificationManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public FloweringPeriodQualificationManager(FloweringPeriodQualificationRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
