using EFarming.Core.Specification;
using EFarming.Core.Specification.Contract;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.QualityModule.MicrolotAggregate
{
    /// <summary>
    /// Microlot Specification
    /// </summary>
    public static class MicrolotSpecification
    {
        /// <summary>
        /// Bies the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>the result</returns>
        public static ISpecification<Microlot> ByCode(string code)
        {
            Specification<Microlot> spec = new TrueSpecification<Microlot>();
            if (!string.IsNullOrEmpty(code))
            {
                spec &= new DirectSpecification<Microlot>(m => m.Code.ToUpper().Equals(code.ToUpper()));
            }
            return spec;
        }
    }
}
