using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.PlantationVarietyAggregate
{
    /// <summary>
    /// PlantationVariety Factory
    /// </summary>
    public static class PlantationVarietyFactory
    {
        /// <summary>
        /// Plantations the variety.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="plantationTypeId">The plantation type identifier.</param>
        /// <returns></returns>
        public static PlantationVariety PlantationVariety(string name, Guid plantationTypeId)
        {
            return new PlantationVariety
            {
                Name = name,
                PlantationTypeId = plantationTypeId
            };
        }
    }
}
