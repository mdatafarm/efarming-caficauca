using EFarming.Core.QualityModule.ChecklistAggregate;
using EFarming.DAL;

namespace EFarming.Repository.QualityModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ChecklistRepository: Repository<Checklist>, IChecklistRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public ChecklistRepository(UnitOfWork uow) : base(uow) { }
    }
}
