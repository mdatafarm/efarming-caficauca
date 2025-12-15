using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.DTO.AdminModule;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Country Interface
    /// </summary>
    public interface ICountryManager: IAdminManager<CountryDTO, Country>
    {
    }
}
