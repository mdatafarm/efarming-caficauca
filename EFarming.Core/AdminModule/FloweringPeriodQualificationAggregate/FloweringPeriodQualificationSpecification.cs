using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate
{
    /// <summary>
    /// Flowring Specification
    /// </summary>
    public static class FloweringPeriodQualificationSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the result</returns>
        public static Specification<FloweringPeriodQualification> FilterByName(string name)
        {
            Specification<FloweringPeriodQualification> spec = new TrueSpecification<FloweringPeriodQualification>();

            if (!string.IsNullOrEmpty(name))
            {
                spec &= new DirectSpecification<FloweringPeriodQualification>(fpq => fpq.Name.ToUpper().Contains(name.ToUpper()));
            }

            return spec;
        }
    }
}
