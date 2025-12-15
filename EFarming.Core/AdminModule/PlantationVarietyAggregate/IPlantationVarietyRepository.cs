using System.Collections.Generic;

namespace EFarming.Core.AdminModule.PlantationVarietyAggregate
{
    /// <summary>
    /// Plantation variety Repository Interface
    /// </summary>
    public interface IPlantationVarietyRepository : IRepository<PlantationVariety>
    {
        List<PlantationVariety> GetFullData();
    }
}
