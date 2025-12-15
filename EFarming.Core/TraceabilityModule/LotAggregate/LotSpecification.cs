using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.TraceabilityModule.LotAggregate
{
    /// <summary>
    /// Lot Specification
    /// </summary>
    public static class LotSpecification
    {
        /// <summary>
        /// Bies the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>the result</returns>
        public static Specification<Lot> ByCode(string code)
        {
            Specification<Lot> spec = new TrueSpecification<Lot>();
            spec &= new DirectSpecification<Lot>(l => l.Code.ToUpper().Equals(code.ToUpper()));
            return spec;
        }
    }
}
