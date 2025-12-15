using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.FarmModule.FamilyUnitAggregate
{
    /// <summary>
    /// FamilyUnit Spacification
    /// </summary>
    public class FamilyUnitSpecification
    {
        /// <summary>
        /// Bies the identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <returns>The result</returns>
        public static Specification<FamilyUnitMember> ByIdentification(string identification)
        {
            Specification<FamilyUnitMember> spec = new TrueSpecification<FamilyUnitMember>();

            spec &= new DirectSpecification<FamilyUnitMember>(fm => fm.Identification.Equals(identification));

            return spec;
        }
    }
}