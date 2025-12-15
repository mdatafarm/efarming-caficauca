using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.AdminModule.VillageAggregate
{
    /// <summary>
    /// Village Specification
    /// </summary>
    public static class VillageSpecification
    {
        /// <summary>
        /// Filters the villages.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="municipalityId">The municipality identifier.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>The result</returns>
        public static Specification<Village> FilterVillages(string name, Guid municipalityId, Guid departmentId)
        {
            Specification<Village> spec = new TrueSpecification<Village>();

            if(!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<Village>(v => v.Name.Contains(name));
            }
            if (municipalityId != null && Guid.Empty != municipalityId)
            {
                spec &= new DirectSpecification<Village>(v => v.MunicipalityId.Equals(municipalityId));
            }
            if (departmentId != null && Guid.Empty != departmentId)
            {
                spec &= new DirectSpecification<Village>(v => v.Municipality.DepartmentId.Equals(departmentId));
            }

            return spec;
        }
    }
}
