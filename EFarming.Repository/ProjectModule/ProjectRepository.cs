using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DAL;

namespace EFarming.Repository.ProjectModule
{
    /// <summary>
    /// Project Repository
    /// </summary>
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public ProjectRepository(UnitOfWork uow) : base(uow) { }
    }
}
