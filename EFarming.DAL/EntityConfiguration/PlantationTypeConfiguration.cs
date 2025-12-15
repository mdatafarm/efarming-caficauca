using EFarming.Core.AdminModule.PlantationTypeAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class PlantationTypeConfiguration : BasicConfiguration<PlantationType>
    {
        public PlantationTypeConfiguration()
        {
            this.Property(pt => pt.Name).IsRequired().HasMaxLength(32);
            this.ToTable("PlantationTypes");
        }
    }
}
