using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;
using System.Linq;

namespace EFarming.Core.ProjectModule.ProjectAggregate
{
    /// <summary>
    /// Project Specification
    /// </summary>
    public static class ProjectSpecification
    {
        /// <summary>
        /// Bies the farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>The result</returns>
        public static Specification<Project> ByFarm(Guid farmId)
        {
            Specification<Project> spec = new TrueSpecification<Project>();
            spec &= new DirectSpecification<Project>(p => p.Farms.Any(f => f.Id.Equals(farmId)));
            return spec;
        }
    }
}
