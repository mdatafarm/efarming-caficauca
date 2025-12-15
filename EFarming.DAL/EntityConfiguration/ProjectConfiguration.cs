using EFarming.Core.ProjectModule.ProjectAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class ProjectConfiguration : BasicConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(p => p.Name).IsRequired();
            Property(p => p.Description).IsRequired();
        }
    }
}
