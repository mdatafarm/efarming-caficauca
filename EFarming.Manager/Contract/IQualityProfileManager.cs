using EFarming.Core.QualityModule.QualityProfileAggregate;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract.AdminModule;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// QualityProfileManager Interface
    /// </summary>
    public interface IQualityProfileManager: IAdminManager<QualityProfileDTO, QualityProfile>
    {
    }
}
