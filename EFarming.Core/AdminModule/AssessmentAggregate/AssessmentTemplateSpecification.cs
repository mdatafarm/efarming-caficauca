using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;

namespace EFarming.Core.AdminModule.AssessmentAggregate
{
    /// <summary>
    /// Specification of assessdment templates
    /// </summary>
    public static class AssessmentTemplateSpecification
    {
        /// <summary>
        /// Bies the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static Specification<AssessmentTemplate> ByType(string type)
        {
            Specification<AssessmentTemplate> spec = new TrueSpecification<AssessmentTemplate>();

            if (!string.IsNullOrEmpty(type))
            {
                spec &= new DirectSpecification<AssessmentTemplate>(at => at.Type.Equals(type));
            }

            return spec;
        }

        /// <summary>
        /// Qualities this instance.
        /// </summary>
        /// <returns></returns>
        public static Specification<AssessmentTemplate> Quality()
        {
            return ByType(typeof(SensoryProfileAssessment).ToString());
        }

        /// <summary>
        /// Sustainabilities this instance.
        /// </summary>
        /// <returns></returns>
        public static Specification<AssessmentTemplate> Sustainability()
        {
            return ByType(typeof(ImpactAssessment).ToString());
        }
    }
}
