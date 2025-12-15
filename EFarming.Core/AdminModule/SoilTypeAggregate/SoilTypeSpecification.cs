using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.SoilTypeAggregate
{
    /// <summary>
    /// Soil Type Specification
    /// </summary>
    public static class SoilTypeSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Specification<SoilType> FilterByName(string name)
        {
            Specification<SoilType> spec = new TrueSpecification<SoilType>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<SoilType>(st => st.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
