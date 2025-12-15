using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.OwnershipTypeAggregate
{
    /// <summary>
    /// OwnershipType Specification
    /// </summary>
    public static class OwnershipTypeSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the data</returns>
        public static Specification<OwnershipType> FilterByName(string name)
        {
            Specification<OwnershipType> spec = new TrueSpecification<OwnershipType>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<OwnershipType>(ot => ot.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
