using EFarming.Core.AdminModule.PlantationStatusAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class PlantationStatusConfiguration : BasicConfiguration<PlantationStatus>
    {
        public PlantationStatusConfiguration()
        {
            this.Property(ps => ps.Name).IsRequired().HasMaxLength(32);
            this.ToTable("PlantationStatuses");
        }
    }
}
