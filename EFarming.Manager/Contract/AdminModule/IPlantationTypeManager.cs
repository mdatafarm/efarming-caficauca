using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using System.Collections.Generic;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IPlantationTypeManager : IAdminManager<PlantationTypeDTO, PlantationType>
    {
        ICollection<PlantationTypeAPIDTO> LoadFullData();
    }
}
