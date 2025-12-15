using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class ProductivityConfiguration : BasicConfiguration<Productivity>
    {
        public ProductivityConfiguration()
        {
            this.Property(p => p.ConservationHectares).IsRequired();
            this.Property(p => p.ForestProtectedHectares).IsRequired();
            this.Property(p => p.InfrastructureHectares).IsRequired();
            this.Property(p => p.ShadingPercentage).IsRequired();
            this.Property(p => p.TotalHectares).IsRequired();

            this.HasRequired(p => p.Farm)
                .WithRequiredDependent(f => f.Productivity);
            this.ToTable("productivities");
        }
    }
}
