using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.FarmStatusAggregate
{
    /// <summary>
    /// FarmStatus Specification
    /// </summary>
    public static class FarmStatusSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the result</returns>
        public static Specification<FarmStatus> FilterByName(string name)
        {
            Specification<FarmStatus> spec = new TrueSpecification<FarmStatus>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<FarmStatus>(fs => fs.Name.Contains(name));
            }
            return spec;
        }
    }
}
