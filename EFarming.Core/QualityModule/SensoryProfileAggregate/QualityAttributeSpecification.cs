using EFarming.Common.Consts;
using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// QualityAttribute Specification
    /// </summary>
    public static class QualityAttributeSpecification
    {
        /// <summary>
        /// Cuantitatives the attributes.
        /// </summary>
        /// <returns>the result</returns>
        public static Specification<QualityAttribute> CuantitativeAttributes()
        {
            Specification<QualityAttribute> spec = new TrueSpecification<QualityAttribute>();

            spec &= new DirectSpecification<QualityAttribute>(qa => qa.TypeOf.Equals(QualityAttributeTypes.RANGE));
            
            return spec;
        }

        /// <summary>
        /// Bies the template.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>the result</returns>
        public static Specification<QualityAttribute> ByTemplate(Guid templateId)
        {
            Specification<QualityAttribute> spec = new TrueSpecification<QualityAttribute>();


	  //qa.AssessmentTemplateId.Equals
            spec &= new DirectSpecification<QualityAttribute>(qa => qa.AssessmentTemplate.Id.Equals(templateId));

            return spec;
        }
    }
}
