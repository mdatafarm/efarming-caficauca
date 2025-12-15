using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract.AdminModule;

namespace EFarming.Manager.Contract
{
    /// <summary>
    /// Project Manager Interface
    /// </summary>
    public interface IProjectManager : IAdminManager<ProjectDTO, Project>
    {
    }
}
