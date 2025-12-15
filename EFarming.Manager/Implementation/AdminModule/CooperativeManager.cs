using EFarming.Core.AdminModule.CooperativeAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// CopperativeManager
    /// </summary>
    public class CooperativeManager : AdminManager<CooperativeDTO, CooperativeRepository, Cooperative>, ICooperativeManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ICooperativeRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="CooperativeManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CooperativeManager(CooperativeRepository repository): base(repository)
        {
            _repository = repository;
        }
    }
}
