using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.DTO.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// AssessmentTemplateManage Interface
    /// </summary>
    public interface IAssessmentTemplateManager : IAdminManager<AssessmentTemplateDTO, AssessmentTemplate>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="includes">The includes.</param>
        /// <returns></returns>
        ICollection<AssessmentTemplateDTO> GetAll<KProperty>(Specification<AssessmentTemplate> filterSpecification, Expression<Func<AssessmentTemplate, KProperty>> orderByExpression, params string[] includes);
    }
}
