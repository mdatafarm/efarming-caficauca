using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.MunicipalityAggregate
{
    /// <summary>
    /// Municipality Specification
    /// </summary>
    public static class MunicipalitySpecification
    {
        /// <summary>
        /// Filters the municipalities.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>The result</returns>
        public static Specification<Municipality> FilterMunicipalities(Guid departmentId, string name)
        {
            Specification<Municipality> specMunicipality = new TrueSpecification<Municipality>();

            if (Guid.Empty != departmentId)
            {
                specMunicipality &= new DirectSpecification<Municipality>(p => p.DepartmentId.Equals(departmentId));
            }
            if (!string.IsNullOrEmpty(name))
            {
                specMunicipality &= new DirectSpecification<Municipality>(p => p.Name.Contains(name));
            }

            return specMunicipality;
        }
    }
}
