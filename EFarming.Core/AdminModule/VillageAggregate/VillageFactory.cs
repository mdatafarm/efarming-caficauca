using System;

namespace EFarming.Core.AdminModule.VillageAggregate
{
    /// <summary>
    /// Village Factory
    /// </summary>
    public static class VillageFactory
    {
        /// <summary>
        /// Villages the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="municipalityId">The municipality identifier.</param>
        /// <returns></returns>
        public static Village Village(string name, Guid municipalityId)
        {
            return new Village
            {
                Name = name,
                MunicipalityId = municipalityId
            };
        }
    }
}
