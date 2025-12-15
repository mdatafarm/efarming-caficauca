using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.CooperativeAggregate
{
    /// <summary>
    /// Cooperative Specification
    /// </summary>
    public static class CooperativeSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The result</returns>
        public static Specification<Cooperative> FilterByName(string name)
        {
            Specification<Cooperative> spec = new TrueSpecification<Cooperative>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<Cooperative>(c => c.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
