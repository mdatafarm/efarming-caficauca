using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.PlantationStatusAggregate
{
    /// <summary>
    /// PlantationStatus Specification
    /// </summary>
    public static class PlantationStatusSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the result</returns>
        public static Specification<PlantationStatus> FilterByName(string name)
        {
            Specification<PlantationStatus> spec = new TrueSpecification<PlantationStatus>();

            if(!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<PlantationStatus>(ps => ps.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
