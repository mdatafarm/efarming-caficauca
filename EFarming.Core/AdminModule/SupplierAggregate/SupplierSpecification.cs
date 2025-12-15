using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.AdminModule.SupplierAggregate
{
    /// <summary>
    /// Supplier Specification
    /// </summary>
    public static class SupplierSpecification
    {
        /// <summary>
        /// Filters the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>the result</returns>
        public static Specification<Supplier> Filter(string name, Guid? countryId)
        {
            Specification<Supplier> filter = new TrueSpecification<Supplier>();
            if (!string.IsNullOrEmpty(name))
            {
                filter &= new DirectSpecification<Supplier>(s => s.Name.ToUpper().Equals(name.ToUpper()));
            }
            if (countryId.HasValue && countryId.Value != Guid.Empty)
            {
                filter &= new DirectSpecification<Supplier>(s => s.CountryId.Equals(countryId.Value));
            }
            return filter;
        }
    }
}
