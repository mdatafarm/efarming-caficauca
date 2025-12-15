using EFarming.Core.AdminModule.PlantationVarietyAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class PlantationVarietyConfiguration : BasicConfiguration<PlantationVariety>
    {
        public PlantationVarietyConfiguration()
        {
            this.Property(pv => pv.Name).IsRequired().HasMaxLength(32);
            this.Property(pv => pv.PlantationTypeId).IsRequired();
            this.HasRequired(pv => pv.PlantationType);
            this.ToTable("PlantationVarieties");
        }
    }
}
