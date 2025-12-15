using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Farm Factory
    /// </summary>
    public static class FarmFactory
    {
        /// <summary>
        /// Farms the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name.</param>
        /// <param name="geolocation">The geolocation.</param>
        /// <param name="farmSubstatusId">The farm substatus identifier.</param>
        /// <param name="cooperativeId">The cooperative identifier.</param>
        /// <param name="ownershipTypeId">The ownership type identifier.</param>
        /// <param name="villageId">The village identifier.</param>
        /// <returns>the farm</returns>
        public static Farm Farm(string code, string name, DbGeography geolocation, Guid farmSubstatusId, Guid cooperativeId,
            Guid ownershipTypeId, Guid villageId)
        {
            var farm = new Farm
            {
                Code = code,
                Name = name,
                GeoLocation = geolocation,
                FarmSubstatusId = farmSubstatusId,
                CooperativeId = cooperativeId,
                OwnershipTypeId = ownershipTypeId,
                VillageId = villageId
            };
            return farm;
        }
    }
}
