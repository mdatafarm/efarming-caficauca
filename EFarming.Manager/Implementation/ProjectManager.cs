using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Repository.ProjectModule;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Project Manager
    /// </summary>
    public class ProjectManager : AdminManager<ProjectDTO, ProjectRepository, Project>, IProjectManager
    {
        /// <summary>
        /// The _project repository
        /// </summary>
        private IProjectRepository _projectRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectManager"/> class.
        /// </summary>
        /// <param name="projectRepository">The project repository.</param>
        public ProjectManager(ProjectRepository projectRepository)
            : base(projectRepository)
        {
            _projectRepository = projectRepository;
        }
    }
}
