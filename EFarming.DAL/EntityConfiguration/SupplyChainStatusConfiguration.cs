using EFarming.Core.AdminModule.SupplyChainAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class SupplyChainStatusConfiguration : BasicConfiguration<SupplyChainStatus>
    {
        public SupplyChainStatusConfiguration()
        {
            this.Property(scs => scs.Name).IsRequired().HasMaxLength(64);

            this.HasMany(scs => scs.SupplyChains)
                .WithOptional(scs => scs.SupplyChainStatus)
                .HasForeignKey(sc => sc.SupplyChainStatusId);

            this.ToTable("supplyChainStatuses");
        }
    }
}
