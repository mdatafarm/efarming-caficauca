using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.MunicipalityAggregate
{
    /// <summary>
    /// Municipality Factory
    /// </summary>
    public static class MunicipalityFactory
    {
        /// <summary>
        /// Municipalities the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>the result</returns>
        public static Municipality Municipality(string name, Guid departmentId)
        {
            var municipality = new Municipality
            {
                Name = name,
                DepartmentId = departmentId
            };
            return municipality;
        }
    }
}
