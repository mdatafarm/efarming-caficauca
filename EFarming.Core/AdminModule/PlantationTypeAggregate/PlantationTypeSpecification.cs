using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.PlantationTypeAggregate
{
    /// <summary>
    /// PlantationTypes Specification
    /// </summary>
    public static class PlantationTypeSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the result</returns>
        public static Specification<PlantationType> FilterByName(string name)
        {
            Specification<PlantationType> spec = new TrueSpecification<PlantationType>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<PlantationType>(pt => pt.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
