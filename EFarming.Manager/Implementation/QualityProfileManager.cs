using EFarming.Core.QualityModule.QualityProfileAggregate;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation.AdminModule;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// QualityProfile Manager
    /// </summary>
    public class QualityProfileManager : AdminManager<QualityProfileDTO, IQualityProfileRepository, QualityProfile>, IQualityProfileManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        IQualityProfileRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="QualityProfileManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public QualityProfileManager(IQualityProfileRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
