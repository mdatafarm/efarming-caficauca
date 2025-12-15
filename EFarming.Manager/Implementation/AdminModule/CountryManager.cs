using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// CountryManager
    /// </summary>
    public class CountryManager : AdminManager<CountryDTO, CountryRepository, Country>, ICountryManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ICountryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CountryManager(CountryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
