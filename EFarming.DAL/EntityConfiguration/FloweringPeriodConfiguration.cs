using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FloweringPeriodConfiguration : BasicConfiguration<FloweringPeriod>
    {
        public FloweringPeriodConfiguration()
        {
            this.Property(fp => fp.PlantationId).IsRequired();
            this.Property(fp => fp.StartDate).IsRequired();

            this.HasRequired(fp => fp.Plantation);

            this.ToTable("floweringPeriods");
        }
    }
}
