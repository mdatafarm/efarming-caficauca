using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Contry Repository
    /// </summary>
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public CountryRepository(UnitOfWork uow) : base(uow) { }
    }
}
