using System.Collections.Generic;

namespace EFarming.Core.AdminModule.PlantationTypeAggregate
{
    /// <summary>
    /// PlantationTypeRepository Interface
    /// </summary>
    public interface IPlantationTypeRepository : IRepository<PlantationType>
    {
        List<PlantationType> GetFullData();
    }
}
