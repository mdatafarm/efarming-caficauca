using EFarming.Core.AdminModule.SupplyChainAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class SupplyChainConfiguration : BasicConfiguration<SupplyChain>
    {
        public SupplyChainConfiguration()
        {
            this.Property(sc => sc.Name).IsRequired();
            this.Property(sc => sc.QualityProfileId).IsOptional();
            this.Property(sc => sc.DepartmentId).IsOptional();
            this.Property(sc => sc.SupplyChainStatusId).IsOptional();
            this.HasMany(sc => sc.Farms)
                .WithOptional(f => f.SupplyChain)
                .HasForeignKey(f => f.SupplyChainId);
            this.ToTable("supplyChains");
        }
    }
}
