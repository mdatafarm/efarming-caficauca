using System;

namespace EFarming.Core.AdminModule.FarmSubstatusAggregate
{
    /// <summary>
    /// FarmSubstatus Factory
    /// </summary>
    public static class FarmSubstatusFactory
    {
        /// <summary>
        /// Farms the substatus.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="farmStatusId">The farm status identifier.</param>
        /// <returns></returns>
        public static FarmSubstatus FarmSubstatus(string name, Guid farmStatusId)
        {
            return new FarmSubstatus
            {
                Name = name,
                FarmStatusId = farmStatusId
            };
        }
    }
}
