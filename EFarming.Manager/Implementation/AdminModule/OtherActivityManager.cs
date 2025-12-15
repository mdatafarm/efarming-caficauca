using EFarming.Core.AdminModule.OtherActivitiesAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// OtherActivity Manager
    /// </summary>
    public class OtherActivityManager : AdminManager<OtherActivityDTO, OtherActivityRepository, OtherActivity>, IOtherActivityManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IOtherActivityRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="OtherActivityManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public OtherActivityManager(OtherActivityRepository repository)
            :base(repository)
        {
            _repository = repository;
        }
    }
}
