using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.CountryAggregate
{
    /// <summary>
    /// Contry Specification
    /// </summary>
    public static class CountrySpecification
    {
        /// <summary>
        /// Filters the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the result</returns>
        public static Specification<Country> Filter(string name)
        {
            Specification<Country> filter = new TrueSpecification<Country>();
            if (!string.IsNullOrEmpty(name))
            {
                filter &= new DirectSpecification<Country>(c => c.Name.ToUpper().Equals(name.ToUpper()));
            }
            return filter;
        }
    }
}
