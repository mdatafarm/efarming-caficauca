using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// Indicator Specification
    /// </summary>
    public static class IndicatorSpecification
    {
        /// <summary>
        /// Bies the template.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>the data</returns>
        public static Specification<Indicator> ByTemplate(Guid templateId)
        {
            Specification<Indicator> spec = new TrueSpecification<Indicator>();

            spec &= new DirectSpecification<Indicator>(i => i.Category.AssessmentTemplateId.Equals(templateId));

            return spec;
        }
    }
}
