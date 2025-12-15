using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using System.Collections.Generic;
namespace EFarming.Core.AdminModule.PlantationTypeAggregate
{
    /// <summary>
    /// PlantationType Factory
    /// </summary>
    public static class PlantationTypeFactory
    {
        /// <summary>
        /// Plantations the type.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static PlantationType PlantationType(string name)
        {
            return new PlantationType
            {
                Name = name,
                PlantationVarieties = new List<PlantationVariety>()
            };
        }
    }
}
