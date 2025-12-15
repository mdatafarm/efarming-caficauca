using AutoMapper;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// Implementation
    /// </summary>
    public class AssessmentTemplateManager : AdminManager<AssessmentTemplateDTO, AssessmentTemplateRepository, AssessmentTemplate>, IAssessmentTemplateManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IAssessmentTemplateRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentTemplateManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AssessmentTemplateManager(AssessmentTemplateRepository repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>ICollection AssessmentTemplateDTO</returns>
        public ICollection<AssessmentTemplateDTO> GetAll<KProperty>(Specification<AssessmentTemplate> filterSpecification, Expression<Func<AssessmentTemplate, KProperty>> orderByExpression, params string[] includes)
        {
            return Mapper.Map<ICollection<AssessmentTemplateDTO>>(_repository.AllMatching(filterSpecification, includes).OrderBy(orderByExpression));
        }
    }
}
