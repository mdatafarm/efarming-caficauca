using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.OtherActivitiesAggregate
{
    /// <summary>
    /// OtherActivity Specification
    /// </summary>
    public static class OtherActivitySpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the result</returns>
        public static Specification<OtherActivity> FilterByName(string name)
        {
            Specification<OtherActivity> spec = new TrueSpecification<OtherActivity>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<OtherActivity>(oa => oa.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
