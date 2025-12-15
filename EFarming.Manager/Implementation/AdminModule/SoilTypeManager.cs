using EFarming.Core.AdminModule.SoilTypeAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// SoilType Manager
    /// </summary>
    public class SoilTypeManager : AdminManager<SoilTypeDTO, SoilTypeRepository, SoilType>, ISoilTypeManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ISoilTypeRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoilTypeManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SoilTypeManager(SoilTypeRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
