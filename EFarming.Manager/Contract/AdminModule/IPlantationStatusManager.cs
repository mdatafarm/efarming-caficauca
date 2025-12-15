using EFarming.Core.AdminModule.PlantationStatusAggregate;
using EFarming.DTO.AdminModule;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IPlantationStatusManager: IAdminManager<PlantationStatusDTO, PlantationStatus>
    {
    }
}
