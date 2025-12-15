using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.ImpactModule.IndicatorAggregate
{
    /// <summary>
    /// Category Specification
    /// </summary>
    public static class CategorySpecification
    {
        /// <summary>
        /// Bies the template.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>the data</returns>
        public static Specification<Category> ByTemplate(Guid templateId)
        {
            Specification<Category> spec = new TrueSpecification<Category>();

            if (templateId != null && templateId != Guid.Empty)
            {
                spec &= new DirectSpecification<Category>(cat => cat.AssessmentTemplateId.Equals(templateId));
            }

            return spec;
        }
    }
}
