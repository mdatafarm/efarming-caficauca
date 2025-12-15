using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class PlantationConfiguration : BasicConfiguration<Plantation>
    {
        public PlantationConfiguration()
        {
            this.Property(p => p.Age).IsRequired();
            this.Property(p => p.EstimatedProduction).IsRequired();
            this.Property(p => p.Hectares).IsRequired();
            this.Property(p => p.TreesDistance).IsRequired();
            this.Property(p => p.GrooveDistance).IsRequired();
            this.Property(p => p.Density).IsRequired();
            this.Property(p => p.NumberOfPlants).IsRequired();
            this.Property(p => p.PlantationStatusId).IsRequired();
            this.Property(p => p.PlantationTypeId).IsRequired();
            this.Property(p => p.PlantationVarietyId).IsRequired();
            this.Property(p => p.ProductivityId).IsRequired();

            this.HasMany(p => p.FloweringPeriods)
                .WithRequired(fp => fp.Plantation)
                .HasForeignKey(fp => fp.PlantationId)
                .WillCascadeOnDelete();
            this.ToTable("plantations");
        }
    }
}
