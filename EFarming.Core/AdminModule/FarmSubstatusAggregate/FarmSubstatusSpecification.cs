using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.AdminModule.FarmSubstatusAggregate
{
    /// <summary>
    /// FarmSubstatusSpecification
    /// </summary>
    public class FarmSubstatusSpecification
    {
        /// <summary>
        /// Filters the farm substatus.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="farmStatusId">The farm status identifier.</param>
        /// <returns>the result</returns>
        public static Specification<FarmSubstatus> FilterFarmSubstatus(string name, Guid farmStatusId)
        {
            Specification<FarmSubstatus> spec = new TrueSpecification<FarmSubstatus>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<FarmSubstatus>(fss => fss.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (farmStatusId != null && Guid.Empty != farmStatusId)
            {
                spec &= new DirectSpecification<FarmSubstatus>(fss => fss.FarmStatusId.Equals(farmStatusId));
            }

            return spec;
        }
    }
}
