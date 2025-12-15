using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using System.Collections.Generic;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IPlantationVarietyManager : IAdminManager<PlantationVarietyDTO, PlantationVariety>
    {
        ICollection<PlantationVarietyAPIDTO> LoadFullData();
    }
}
