using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// CriteriaOption Specification
    /// </summary>
    public static class CriteriaOptionSpecification
    {
        /// <summary>
        /// Ins the specified ids.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>the result</returns>
        public static Specification<CriteriaOption> In(IEnumerable<Guid> ids)
        {
            Specification<CriteriaOption> spec = new TrueSpecification<CriteriaOption>();

            spec &= new DirectSpecification<CriteriaOption>(co => ids.Contains(co.Id));

            return spec;
        }
    }
}
