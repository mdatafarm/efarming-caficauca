using EFarming.Core.AdminModule.FarmStatusAggregate;
using EFarming.DTO.AdminModule;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// FarmStatusManager Interface
    /// </summary>
    public interface IFarmStatusManager : IAdminManager<FarmStatusDTO, FarmStatus>
    {
    }
}
