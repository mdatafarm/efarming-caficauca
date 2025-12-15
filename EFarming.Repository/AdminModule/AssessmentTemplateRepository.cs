using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// AssessmentTemplate Repository
    /// </summary>
    public class AssessmentTemplateRepository : Repository<AssessmentTemplate>, IAssessmentTemplateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentTemplateRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public AssessmentTemplateRepository(UnitOfWork uow) : base(uow) { }
    }
}
